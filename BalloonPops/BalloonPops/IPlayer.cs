//-----------------------------------------------------------------------
// <copyright file="IPlayer.cs" company="SoftUni">
//  Copyright (c) 2015 SoftUni. All rights reserved.
// </copyright>
// <author>Team "Darby"</author>
//-----------------------------------------------------------------------
namespace BalloonPops
{
    /// <summary>
    /// Interface for Player
    /// </summary>
    public interface IPlayer
    {
        string Name { get; }

        int Score { get; }
    }
}