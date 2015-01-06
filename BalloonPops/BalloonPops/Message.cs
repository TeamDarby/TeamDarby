//-----------------------------------------------------------------------
// <copyright file="Message.cs" company="SoftUni">
//  Copyright (c) 2015 SoftUni. All rights reserved.
// </copyright>
// <author>Team "Darby"</author>
//-----------------------------------------------------------------------
namespace BalloonPops
{
    /// <summary>
    /// Display all game messages.
    /// </summary>
    public static class Message
    {
        /// <summary>
        /// Welcome message.
        /// </summary>
        public const string Welcome = 
        @"Welcome to “Balloons Pops” game. Please try to pop the balloons.
        Enter the row and column below separated by space. Example:'2 3'";


        /// <summary>
        /// Informational message for game commands.
        /// </summary>
        public const string Info =
        @"Use 'top' to view the top scoreboard, 'restart' to start a new game and 
        'exit' to quit the game.";

        /// <summary>
        /// Message for illegal move.
        /// </summary>
        public const string IllegalMove = "Illegal move: Cannot pop missing ballon!";

        /// <summary>
        /// Message to player to enter row and column value.
        /// </summary>
        public const string EnterValues = "Enter a row and column here --> ";

        /// <summary>
        /// Message for wrong input.
        /// </summary>
        public const string WrongInput = "Wrong Input!";

        /// <summary>
        /// Message that inform player for successfully entering in top players list.
        /// </summary>
        public const string Success = "Congratulations, you have popped more ballons than the other players!";

        /// <summary>
        /// Message to player to enter his name for top players list.
        /// </summary>
        public const string EnterName = "Please enter your name for the top scoreboard: ";

        /// <summary>
        /// Message that wish luck.
        /// </summary>
        public const string NotBestScore = "Sorry, your moves are more that last best player! Better luck next time!";
    }
}