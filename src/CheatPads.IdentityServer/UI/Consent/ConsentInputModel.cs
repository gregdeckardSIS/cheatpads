﻿namespace CheatPads.IdentityServer.UI.Consent
{
    using System.Collections.Generic;

    public class ConsentInputModel
    {
        public IEnumerable<string> ScopesConsented { get; set; }
        public bool RememberConsent { get; set; }
    }
}
