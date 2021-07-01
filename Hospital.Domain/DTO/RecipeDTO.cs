using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.DTO
{
    public class RecipePatientDTO
    {

        public int PatientId { get; set; }
        public int RecipeId { get; set; }

        public RecipePatientDTO() { }
    }
}
