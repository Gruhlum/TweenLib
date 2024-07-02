using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public enum AnimationType { EaseIn, EaseOut, EaseInOut }
    public enum Curve { Sine, Quad, Cubic, Quart, Quint, Expo, Circ, Back, Elastic, Bounce }
    public enum Mode { Addition, Multiply, Set }
    public enum Space { Local, World }
    public enum TargetVector { Position, Rotation, Scale }
    public enum LoopMode { Mirror, Repeat }
    public enum FadeMode { FadeIn, FadeOut }
    public enum Position { Start, End }
}