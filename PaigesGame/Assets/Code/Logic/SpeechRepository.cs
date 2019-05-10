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
                new Speech("I have been looking for you, I need your help.", 3.25f),
                new Speech("Paige is trapped in the bedroom, we need to free her!", 3.25f)
            };
        }

        public static List<Speech> GetNearDoorSpeechNoneCollected()
        {
            return new List<Speech>()
            {
                new Speech("Damn the door is locked." + Environment.NewLine + "We need to kill the demons to unlock it!", 3),
                new Speech("Try the kitchen or the bathroom.", 2.5f)
            };
        }

        public static List<Speech> GetNearDoorSpeechOneMore()
        {
            return new List<Speech>()
            {
                new Speech("Damn the door is locked." + Environment.NewLine + "We need to kill one more of the demons to unlock it!", 3),
                new Speech("Try the kitchen or the bathroom.", 2.5f)
            };
        }

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
