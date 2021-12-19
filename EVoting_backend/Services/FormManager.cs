using EVoting_backend.API.Request;
using EVoting_backend.DB;
using EVoting_backend.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    var newSFRes = await _appDbContext.SubForm.AddAsync(new SubForm { Type = sf.Type, Form = newFormRes.Entity });
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
                await _appDbContext.Vote.AddAsync(new Vote { Data = voteRequest.VoteData });
                await _appDbContext.UserVotes.AddAsync(new UserVoted { Form = form, User = user });
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
