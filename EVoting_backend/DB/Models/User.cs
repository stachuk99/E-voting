using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EVoting_backend.DB.Models
{
    public class User : IdentityUser<Guid>
    {
    }
}
