﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Shared.Extensions
{
    public static class EnumExtensions
    {

        //enumın içine vereceğimiz tipin display atribute u null değilse name ini ver.
        public static string GetDisplayName(this Enum value)
        {
            string result = value
                .GetType()
                .GetMember(value.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                ?.GetName();
            return result;
        }
    }
}

