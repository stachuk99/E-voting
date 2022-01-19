using EVoting_backend.API.Request;
using EVoting_backend.API.Response;
using EVoting_backend.DB;
using EVoting_backend.DB.Models;
using EVoting_backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EVoting_backend.Services
{
    public class FormManager
    {
        private readonly AppDbContext _appDbContext;

        public FormManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AddForm(PostFormRequest formRequest)
        {
            try
            {
                var newFormRes = await _appDbContext.Form.AddAsync(new Form { Name = formRequest.Name, From = formRequest.From, To = formRequest.To });
                foreach (var sf in formRequest.SubForms)
                {
                    var newSFRes = await _appDbContext.SubForm.AddAsync(new SubForm {Name = sf.Name, ChoicesLimit = sf.ChoicesLimit, Form = newFormRes.Entity });
                    foreach (var fo in sf.Options)
                    {
                        var newFORes = await _appDbContext.FormOption.AddAsync(new FormOption { Ident = fo.Ident, Name = fo.Name, SubForm = newSFRes.Entity });
                    }
                }
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddVote(string userMail, PostVoteRequest voteRequest)
        {
            var user = await _appDbContext.Users.Include(p => p.Votes).FirstOrDefaultAsync(p => p.Email == userMail);
            var form = await _appDbContext.Form.FindAsync(voteRequest.FormId);
            if(form == null || user == null) return false;
            if(user.Votes.Any(p => p.FormId == voteRequest.FormId)) return false;
            try
            {
                await _appDbContext.Vote.AddAsync(new Vote { Data = voteRequest.VoteData, Form = form, iv= user.iv, Secret=user.Secret });
                await _appDbContext.UserVotes.AddAsync(new UserVoted { Form = form, User = user });
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<FormDefinitionsResponse[]> ListForms(DateTime date)
        {
            var forms = await _appDbContext.Form
                .Include(p => p.SubForms)
                .Where(p => p.From <= date && p.To >= date)
                .Select(p => new FormDefinitionsResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    From = p.From,
                    To = p.To,
                    SubForms = p.SubForms
                    .Select(
                        r => new SubFormResponse
                        {
                            Id = r.Id,
                            Name = r.Name,
                            ChoicesLimit = r.ChoicesLimit,
                            Options = r.Options
                                .Select(
                                    x => new FormOptionResponse
                                    {
                                        Ident = x.Ident,
                                        Name = x.Name,
                                    }
                                ).ToList()
                        }
                    ).ToList()
                }).ToArrayAsync();

            return forms;
        }

        public async Task CountVotes(int id)
        {
            var votes = _appDbContext.Vote.Where(p => p.FormId == id).ToList();
            var formDB = _appDbContext.Form.Find(id);
            foreach (var rawVote in votes)
            {
                try
                {
                    byte[] biv = Encoding.UTF8.GetBytes(rawVote.iv);
                    byte[] bSecret = Convert.FromBase64String(rawVote.Secret);
                    byte[] bdata = Convert.FromBase64String(rawVote.Data);
                    string ddd = Convert.ToBase64String(bdata);
                    string ddc = Convert.ToBase64String(bSecret);
                    string decrypted = DecryptStringFromBytes(bdata, bSecret, biv);
                    Console.WriteLine(decrypted);
                    var vote = JsonSerializer.Deserialize<VoteModel>(decrypted);
                    Console.WriteLine(vote);
                    if (!isVoteValid(vote)) 
                        continue;
                    foreach(var sfDB in formDB.SubForms)
                    {
                        var sf = vote.SubForms.First(p => p.Id == sfDB.Id);
                        int choices = 0;
                        int countedOptions = 0;
                        foreach(var optDB in sfDB.Options)
                        {
                            var opt = sf.Options.FirstOrDefault(p => p.Ident == optDB.Ident);
                            if (opt.Checked) optDB.Count++;
                        }
                    }
                }catch(Exception e) 
                {
                    Console.WriteLine(e.Message);
                }
            }
            await _appDbContext.SaveChangesAsync();
        }

        private bool isVoteValid(VoteModel vote)
        {
            var voteDB = _appDbContext.Form.Include(p => p.SubForms).ThenInclude(p => p.Options).FirstOrDefault(p => p.Id == vote.FormId);
            if(voteDB == null) return false;
            foreach(var sfDB in voteDB.SubForms)
            {
                var sf = vote.SubForms.FirstOrDefault(s => s.Id == sfDB.Id);
                if(sf == null) return false;
                for(int i=1; i<=sfDB.Options.Count; i++)
                {
                    var optDB = sfDB.Options.FirstOrDefault(p => p.Ident==i);
                    if(optDB == null) 
                        return false;
                    var opt = sf.Options.FirstOrDefault(p => p.Ident == i);
                    if (opt == null) 
                        return false;
                }
                if (sf.Options.Count(p => p.Checked) > sfDB.ChoicesLimit) return false;
            }
            return true;
        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

    }
}
