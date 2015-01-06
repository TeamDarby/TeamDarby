//-----------------------------------------------------------------------
// <copyright file="BalloonPopsGame.cs" company="SoftUni">
//  Copyright (c) 2015 SoftUni. All rights reserved.
// </copyright>
// <author>Team "Darby"</author>
//-----------------------------------------------------------------------
namespace BalloonPops
{
    /// <summary>
    /// Represent the game "Balloon Pops".
    /// </summary>
    public static class BalloonPopsGame
    {
        /// <summary>
        /// Star playing the game "Balloon Pops".
        /// </summary>
        public static void Main()
        {
            BalloonPops game = new BalloonPops();
            game.Play();
        }
    }
}