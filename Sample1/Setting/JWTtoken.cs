﻿namespace Sample1.Setting {
    public class JWTtoken {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessExpiration { get; set; }
    }
}
