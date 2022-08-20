using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public enum ResignState
    {
        waitForApproval,    //等待审批
        agreed,             //同意
        refused             //被拒绝
    }
}
