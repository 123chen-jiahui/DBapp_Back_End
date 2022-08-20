using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public enum BreakState
    {
        waitForApproval,    //等待审批
        agreed,               //审批通过
        refused             //被拒绝
    }
}
