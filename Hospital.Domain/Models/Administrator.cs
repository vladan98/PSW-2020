using Hospital.Domain.Models.Users;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    [Table("Administrators")]
    public class Administrator : RegisteredUser
    {
    }
}
