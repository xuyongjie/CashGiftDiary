using System;
using System.Collections.Generic;
using System.Text;

namespace ViewLayer
{
    public interface IRegisterView
    {
        void setRegisterResult(bool result, string error);
    }
}
