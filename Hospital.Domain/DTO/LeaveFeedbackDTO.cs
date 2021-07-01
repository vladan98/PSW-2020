using Hospital.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.DTO
{
    public class LeaveFeedbackDTO
    {
        public int PatientId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public LeaveFeedbackDTO() { }

    }
}
