using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Logic
{
    public static class SpeechRepository
    {
        // returns a list of what to output with timings
        public static List<Speech> GetMattFollowJoJoSpeech()
        {
            return new List<Speech>()
            {
                new Speech("There you are JoJo!", 2.5f),
                new Speech("I have been looking for you, I need your help.", 4f),
                new Speech("Paige is trapped in the bedroom, we need to free her!", 4f)
            };
        }

        public static List<Speech> GetExitLoungeSpeechNoneCollected()
        {
            return new List<Speech>()
            {
                new Speech("Damn looks like the bedroom door is locked.", 3f),
                new Speech("We need to kill the demons to unlock it!", 2.25f),
                new Speech("They are in the kitchen and the bathroom.", 2.5f)
            };
        }

        //public static List<Speech> GetNearDoorSpeechOneMore()
        //{
        //    return new List<Speech>()
        //    {
        //        new Speech("Damn the door is locked." + Environment.NewLine + "We need to kill more of the demons to unlock it!", 3)
        //    };
        //}

        public static List<Speech> GetEnterBedroomSpeech()
        {
            return new List<Speech>()
            {
                new Speech("Paige is in that Jar, quick try to break it JoJo!", 3)
            };
        }

        public static List<Speech> GetJoJoBreakJarFailedSpeech()
        {
            return new List<Speech>()
            {
                new Speech("Damn, couldn't break it?", 2),
                new Speech("Then I think I have an idea!", 2)
            };
        }

        public static List<Speech> GetDemonDied(int demonsLeft)
        {
            return new List<Speech>()
            {
                new Speech("Wow, didn't expect that. Well done, only " + demonsLeft + " demons left!", 2)
            };
        }

        public static List<Speech> GetNoMoreDemons()
        {
            return new List<Speech>()
            {
                new Speech("Nice one Jojo, no more demons the door is open." + Environment.NewLine + "Let's save Paige!", 2)
            };
        }

        public static List<Speech> SheSaidYesFuckYeah()
        {
            return new List<Speech>()
            {
                new Speech("I Love you like nothing else, I can't wait to spend my life with you!!!!!!!!", 1000)
            };
        }

        public static List<Speech> PaigeFreedom()
        {
            return new List<Speech>()
            {
                new Speech("Finaaalllllly!", 3)
            };
        }
    }

    public class Speech
    {
        public Speech(string speechText, float speechTimeSeconds)
        {
            this.SpeechText = speechText;
            this.SpeechTimeSeconds = speechTimeSeconds;
        }

        public string SpeechText { get; set; }
        public float SpeechTimeSeconds { get; set; }
    }
}
