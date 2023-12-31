﻿namespace ERNI.BerlinSpartans.Hackathon.Services.PiCarXClient.Model
{
    public struct Movement
    {
        public bool Forward { get; set; }
        public bool Backward { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool TurnHeadLeft { get; set; }
        public bool TurnHeadRight { get; set; }
        public bool StartLane { get; set; }

        public override string ToString()
        {
            return $@"Forward: {Forward}, 
                      Backward: {Backward}, 
                      Left: {Left}, 
                      Right: {Right}, 
                      TurnHeadLeft: {TurnHeadLeft}, 
                      TurnHeadRight: {TurnHeadRight}";
        }
    }
}
