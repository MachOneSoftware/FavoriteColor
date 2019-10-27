using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using System;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace MachOneSoftware.FavoriteColor
{
    public class Function
    {
        private const string HelpMessage = "Try asking for a color.";
        private const string StopMessage = "Goodbye color seeker";
        private const string Error_Unknown = "Sorry, something went wrong. Try asking for a color.";

        private readonly string[] coolColors =
        {
            "Air Force Blue",
            "Antique bronze",
            "Alice Blue", 
            "Amazon Orange", 
            "American Rose", 
            "Android Green", 
            "Antique Brass", 
            "Antique Fuchsia", 
            "Antique White", 
            "Apple Green", 
            "Aqua", 
            "Aquamarine", 
            "Army Green", 
            "Arsenic", 
            "As Dark As My Soul Black", 
            "Azure", 
            "Baby Throw Up Green", 
            "Beige", 
            "Bisque", 
            "Black", 
            "Blanched Almond", 
            "Blue", 
            "Blue But Every Time It's Blue It Turns Green", 
            "Blue Violet", 
            "Bluish Green", 
            "Brown", 
            "Burlywood", 
            "Cadet Blue", 
            "Caput Mortuum", 
            "Carrot Parrot", 
            "Chartreuse", 
            "Chocolate", 
            "Communist Red", 
            "Coral", 
            "Cornflower Blue", 
            "Cornsilk", 
            "Corsica Red", 
            "Crimson", 
            "Cyan", 
            "Da Ba Dee Blue", 
            "Dark Blue", 
            "Dark Cyan", 
            "Dark Goldenrod", 
            "Dark Gray", 
            "Dark Green", 
            "Dark Grey", 
            "Dark Khaki", 
            "Dark Magenta", 
            "Dark OliveGreen", 
            "Dark Orange", 
            "Dark Orchid", 
            "Dark Red", 
            "Dark Salmon", 
            "Dark SeaGreen", 
            "Dark SlateBlue", 
            "Dark SlateGray", 
            "Dark Turquoise", 
            "Dark Violet", 
            "Deep Pink", 
            "Deep SkyBlue", 
            "Delorean Gray", 
            "Dim Grey", 
            "Dodger Blue", 
            "Dog Poop Brown", 
            "Electric Orange", 
            "Emoji Yellow", 
            "Falu Red", 
            "Firebrick Red", 
            "Flamingo", 
            "Floral White", 
            "Forest Green", 
            "Fuchsia", 
            "Fuzzy Caterpillar Brown", 
            "Fuzzy Wuzzy Brown", 
            "Gainsboro", 
            "Gamboge Yellow", 
            "Ghost White", 
            "Gold", 
            "Goldenrod", 
            "Goth Black", 
            "Gray With An A.", 
            "Green", 
            "Green But It's We Are Number One", 
            "Green Dot Com", 
            "Green Yellow", 
            "Grey", 
            "Grey With An E.", 
            "Honeydew", 
            "Hot Pink", 
            "House Fly Black", 
            "I Guess That's Why They Call It The Blues Blue", 
            "Indian Red", 
            "Indigo", 
            "Ivory", 
            "Khaki", 
            "Kitty Cat Black", 
            "Kitty Cat Calico", 
            "Kitty Cat Orange", 
            "Kitty Cat Tortoiseshell", 
            "Lavender", 
            "Lavender Blush", 
            "Lawn Green", 
            "Lemon Chiffon", 
            "Libertarian Gold", 
            "Light Blue", 
            "Light Coral", 
            "Light Cyan", 
            "Light Goldenrod Yellow", 
            "Light Gray", 
            "Light Green", 
            "Light Grey", 
            "Light Pink", 
            "Light Salmon", 
            "Light Sea Green", 
            "Light Sky Blue", 
            "Light Slate Gray", 
            "Light Steel Blue", 
            "Light Yellow", 
            "Lime", 
            "Lime Green", 
            "Linen", 
            "Magenta", 
            "Malachite", 
            "Malachite Green", 
            "Maroon", 
            "Mauve", 
            "Medium Aquamarine", 
            "Medium Blue", 
            "Medium Orchid", 
            "Medium Purple", 
            "Medium Sea Green", 
            "Medium Slate Blue", 
            "Medium Spring Green", 
            "Medium Turquoise", 
            "Medium Violet Red", 
            "Midnight Blue", 
            "Mint Cream", 
            "Misty Rose", 
            "Moccasin", 
            "Navajo White", 
            "Navy", 
            "Nebraska Cornhusker Cream", 
            "Nebraska Cornhusker Scarlet", 
            "Neon Pink", 
            "Nomad Red", 
            "Old Lace", 
            "Olive", 
            "Olive Drab", 
            "Orange", 
            "Orange But It's All Star By Smash Mouth", 
            "Orange Is The New Black Black", 
            "Orange Red", 
            "Orchid", 
            "Pale Goldenrod", 
            "Pale Green", 
            "Pale Turquoise", 
            "Pale Violet Red", 
            "Papaya Whip", 
            "Peach Puff", 
            "Peru", 
            "Pink", 
            "Plum", 
            "Powder Blue", 
            "Purple", 
            "Purple Haze Purple", 
            "Purple Mountain Majesties Purple", 
            "Razzmatazz", 
            "Real Men Wear Pink Pink", 
            "Rebecca Purple", 
            "Red", 
            "Red Red Wine", 
            "Rocket's Red Glare Red", 
            "Rosy Brown", 
            "Royal Blue", 
            "Saddle Brown", 
            "Salmon", 
            "Sandy Brown", 
            "Screaming Green", 
            "Sea Green", 
            "Seashell", 
            "Sienna", 
            "Silver", 
            "Simpsons Yellow", 
            "Sky Blue", 
            "Slate Blue", 
            "Slate Gray", 
            "Snow", 
            "South Dakota Coyotes Red", 
            "South Dakota Coyotes White", 
            "Spanish Yellow", 
            "Spring Green", 
            "Steel Blue", 
            "Stupid Yellow", 
            "Tan", 
            "Teal", 
            "The Grass Is Greener On The Other Side Green", 
            "This Color Will Offend You, So I Won't Say It", 
            "Thistle", 
            "Tomato", 
            "Turquoise", 
            "Violet", 
            "Wheat", 
            "White", 
            "White Christmas White", 
            "White Smoke", 
            "Wine Red", 
            "Xanadu Green-Gray", 
            "Yellow", 
            "Yellow Green",
            "peacock blue",
            "crimson red",
            "queen pink",
            "cosmic latte",
            "Azure blue",
            "navy blue",
            "Flamingo purple",
            "Frost White",
            "spring green",
            "Fallow",
            "maroon",
            "baby pink",
            "baby blue",
            "blood red",
            "navy blue",
            "peacock green",
            "ocean blue",
            "saffron",
            "fuchsia pink",
            "lime green",
            "Midnight Blue"
        };

        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            Shared.RequestId = context.AwsRequestId;
            Shared.Logger = context.Logger;
            var response = new SkillResponse()
            {
                Version = "1.0.0",
                Response = new ResponseBody() { ShouldEndSession = true }
            };
            IOutputSpeech output = null;

            try
            {
                var requestType = input.GetRequestType();
                if (requestType == typeof(LaunchRequest))
                    response.Response.ShouldEndSession = HandleRandomColorIntent(out output);
                else if (requestType == typeof(SessionEndedRequest))
                    output = Shared.GetOutput(StopMessage);
                else if (requestType == typeof(IntentRequest))
                {
                    var intentRequest = (IntentRequest)input.Request;
                    switch (intentRequest.Intent.Name)
                    {
                        case "AMAZON.CancelIntent":
                            output = Shared.GetOutput(StopMessage);
                            break;
                        case "AMAZON.StopIntent":
                            output = Shared.GetOutput(StopMessage);
                            break;
                        case "AMAZON.HelpIntent":
                            response.Response.ShouldEndSession = false;
                            output = Shared.GetOutput(HelpMessage);
                            break;
                        case "RandomColorIntent":
                            response.Response.ShouldEndSession = HandleRandomColorIntent(out output);
                            break;
                        default:
                            response.Response.ShouldEndSession = false;
                            output = Shared.GetOutput(HelpMessage);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.LogError("FunctionHandler", $"input = {input}; context = {context}", ex);
                output = Shared.GetOutput(Error_Unknown);
                response.Response.ShouldEndSession = false;
            }
            finally
            {
                response.Response.OutputSpeech = output;
            }
            return response;
        }

        /// <summary>
        /// Handler for RandomColorIntent. Returns a value indicating whether to end the session. Returns the inner response as an out parameter.
        /// </summary>
        /// <param name="output">Response output speech.</param>
        /// <returns></returns>
        private bool HandleRandomColorIntent(out IOutputSpeech output)
        {
            try
            {
                var r = new Random((int)DateTime.Now.Ticks);
                output = Shared.GetOutput(coolColors[r.Next(coolColors.Length)]);
                return true;
            }
            catch (Exception ex)
            {
                Shared.LogError("HandleRandomColorIntent", "output", ex);
                output = Shared.GetOutput(Error_Unknown);
                return false;
            }
        }
    }
}
