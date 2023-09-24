using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController.Domain.Dtos;

public record TalkDto
{
    public string? Text { get; set; }
}
