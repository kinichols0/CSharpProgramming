using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Enums
{
    public enum Grade
    {
        A = 4,
        B = 3,
        C = 2,
        D = 1,
        F = 0
    }

    public enum ClassStanding
    {
        Freshman,
        Sophomore,
        Junior,
        Senior,
        Graduate
    }

    public enum ConnectionStatus
    {
        Open,
        Closed
    }

    /// <summary>
    /// Enums set to values AND, OR, NOT and XOR bitwise operations
    /// can be performed on them
    /// </summary>
    [Flags]
    public enum DaysOfWeek
    {
        None = 0,
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 10,
        Friday = 20,
        Saturday = 40
    }
}
