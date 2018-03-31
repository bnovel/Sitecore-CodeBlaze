using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC.CodeBlaze.Foundation.SitecoreExtensions.Extensions
{
    public static class Extensions
    {
        public static Boolean IsNull(this object obj)
        {
            if (obj == null)
                return true;
            return false;
        }
    }
}