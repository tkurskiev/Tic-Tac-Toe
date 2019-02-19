﻿using System;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// The type of value a cell in game is currently at
    /// </summary>
    [Flags]
    public enum MarkType
    {
        /// <summary>
        /// The cell hasn't been clicked yet
        /// </summary>
        Free,
        /// <summary>
        /// The cell is a O
        /// </summary>
        Nought,
        /// <summary>
        /// The cell is an X
        /// </summary>
        Cross
    }
}
