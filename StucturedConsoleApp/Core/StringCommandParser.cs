using System.Linq;

namespace StucturedConsoleApp.Core
{
    public interface IStringCommandParser
    {
        GenericCommand ParseArgs(string[] args);
    }

    public class StringCommandParser : IStringCommandParser
    {
        public GenericCommand ParseArgs(string[] args)
        {
            GenericCommand genericArg = new GenericCommand();

            var argsList = args.ToList();



            foreach (string text in argsList)
            {
                var processedText = text;

                if (!processedText.StartsWith("-"))
                {
                    genericArg.NoneOptArguments.Add(processedText);
                }
                else
                {
                    processedText = processedText.TrimStart('-');

                    var keyValue = processedText.Split(':');

                    if (keyValue.Length == 1)
                    {
                        genericArg.Arguments.Add(processedText, "");
                    }

                    if (keyValue.Length == 2)
                    {
                        genericArg.Arguments.Add(keyValue[0], keyValue[1]);
                    }
                }
            }
            return genericArg;
        }
    }
}
