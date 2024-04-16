using System.Linq;
using System.IO;

namespace String_Stuff
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String line;
            String[] outputFiles = { "blowUp.txt", "maxRun.txt", "shrink.txt" };
            try
            {
                StreamReader sr = new StreamReader("input.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    foreach (String outputFile in outputFiles)
                    {
                        // Checking for valid write path
                        try
                        {
                            StreamWriter sw = new StreamWriter(outputFile, true);
                            // Catches for invalid input data.
                            try
                            {
                                String result = StringMethodCaller(outputFile, line);
                                sw.WriteLine(result);
                                sw.Close();
                            }
                            catch
                            {
                                sw.WriteLine("Invalid Input!");
                                sw.Close();
                            }
                        }
                        catch
                        {
                            Console.WriteLine($"File path: {outputFile} could not be found!");
                        }


                    }
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch
            {
                Console.WriteLine("Incorrect input file path!");
            }

        }

        static String StringMethodCaller(String writeName, String value)
        {
            switch (writeName)
            {
                case "blowUp.txt":
                    return BlowUp(value);
                case "maxRun.txt":
                    return Convert.ToString(MaxRun(value));
                default:
                    return Shrink(value);
            }
        }

        static String BlowUp(string s)
        {
            String blownUpString = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (Char.IsDigit(s[i]))
                {
                    int repetitions = (int)Char.GetNumericValue(s[i]);
                    String blownUpSegment = string.Concat(Enumerable.Repeat(s[i + 1], repetitions));
                    blownUpString += blownUpSegment;
                }
                else
                {
                    blownUpString += s[i];
                }
            }
            return blownUpString;
        }

        static int MaxRun(string s)
        {
            Char previousChar = s[0];
            int repetitions = 0;
            List<int> runs = new List<int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == previousChar)
                {
                    repetitions++;
                }
                else
                {
                    runs.Add(repetitions);
                    repetitions = 1;
                }
                previousChar = s[i];

            }
            runs.Sort();
            return runs.Last();
        }

        static String Shrink(string s)
        {
            String shrunkenString = "";
            Char previousChar = s[0];
            int repetitions = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == previousChar)
                {
                    repetitions++;
                }
                else
                {
                    if (repetitions == 1)
                    {
                        shrunkenString += s[i-1];
                    }
                    else
                    {
                        shrunkenString += Convert.ToString(repetitions-1) + s[i-1];
                    }
                    repetitions = 1;
                }
                previousChar = s[i];
            }

            if (repetitions == 1)
            {
                return shrunkenString += previousChar;
            }
            else
            {
                return shrunkenString += Convert.ToString(repetitions - 1) + previousChar;
            }
        }

    }


}
