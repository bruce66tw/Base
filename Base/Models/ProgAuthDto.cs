﻿namespace Base.Models
{
    /// <summary>
    /// program auth
    /// </summary>
    public class ProgAuthDto
    {
        //program id
        public string ProgCode { get; set; }

        //program name
        public string ProgName { get; set; }

        public string AuthStr { get; set; }
    }
}
