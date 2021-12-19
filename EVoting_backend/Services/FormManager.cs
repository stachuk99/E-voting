using EVoting_backend.API.Request;
using EVoting_backend.DB;
using EVoting_backend.DB.Models;
using System;
using System.Collections.Generic;
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
    }
}
