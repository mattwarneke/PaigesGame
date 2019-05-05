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
                new Speech("There you are JoJo." + Environment.NewLine + "I have been looking for you.", 3),
                new Speech("I need your help, Paige is trapped and needs to be freed!", 3)
            };
        }

        public static List<Speech> GetNearDoorSpeechNoneCollected()
        {
            return new List<Speech>()
            {
                new Speech("Damn the door is locked." + Environment.NewLine + "We need to find X to unlock it!", 3)
            };
        }

        public static List<Speech> GetNearDoorSpeechOneMore()
        {
            return new List<Speech>()
            {
                new Speech("Damn the door is locked." + Environment.NewLine + "One more X to unlock it!", 3)
            };
        }

        public static List<Speech> GetEnterBedroomSpeech()
        {
            return new List<Speech>()
            {
                new Speech("Paige is in that Jar, quick try to break it JoJo!", 3)
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
