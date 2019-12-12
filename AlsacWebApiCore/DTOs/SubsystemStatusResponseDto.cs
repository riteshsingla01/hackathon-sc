using System;
using System.Collections.Generic;
using System.Text;

namespace AlsacWebApiCore.DTOs
{
    public class SubsystemStatusResponseDto
    {
        public List<SubsystemStatusResponseItem> Subsystems { get; set; }
        public bool OverallSystemUp { get; set; }
    }
}
