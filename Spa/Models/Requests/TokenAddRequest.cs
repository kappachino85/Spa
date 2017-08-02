﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spa.Models.Requests
{
    public class TokenAddRequest
    {

        public Guid TokenGuid { get; set; }

        public int SiteUserId { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}