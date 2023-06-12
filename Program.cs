using System.Text;

var arglist = args.ToList();

if (arglist.IndexOf("-h") != -1)
{
    foreach (var line in new[]
             {
        "-h\tShow help.",
        "-l LENGTH\tSet the length of the generated passwords.",
        "-n NUMBER\tSet the number of the generated passwords.",
        "-x CHARACTERS\tExclude given characters from the generated passwords.",
        "",
        "-nosym\tDon't include Symbols in !@#$%^&*+-=_()[]{}?.",
        "-nonum\tDon't include Numbers.",
        "-noup\tDon't include Upper Letters.",
        "-nolow\tDon't include Lower Letters."
             })
    {
        Console.WriteLine(line);
    }

    return;
}

var lFlag = arglist.IndexOf("-l");
var nFlag = arglist.IndexOf("-n");
var xFlag = arglist.IndexOf("-x");
var length = lFlag == -1 ? 16 : int.Parse(arglist[lFlag + 1]);
var useSym = arglist.IndexOf("-nosym") == -1;
var useNum = arglist.IndexOf("-nonum") == -1;
var useUpper = arglist.IndexOf("-noup") == -1;
var useLower = arglist.IndexOf("-nolow") == -1;
var repeat = nFlag == -1 ? 1 : int.Parse(arglist[nFlag + 1]);
var excludedChars = new HashSet<char>(xFlag == -1 ? "" : arglist[xFlag + 1]);

const string SYMBOLS = "!@#$%^&*+-=_()[]{}?.";
const string NUMBERS = "0123456789";
const string UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
const string LOWER = "abcdefghijklmnopqrstuvwxyz";

var sb = new StringBuilder();

if (useSym)
    foreach (var c in SYMBOLS.Where(c => !excludedChars.Contains(c)))
        sb.Append(c);

if (useNum)
    foreach (var c in NUMBERS.Where(c => !excludedChars.Contains(c)))
        sb.Append(c);
if (useUpper)
    foreach (var c in UPPER.Where(c => !excludedChars.Contains(c)))
        sb.Append(c);
if (useLower)
    foreach (var c in LOWER.Where(c => !excludedChars.Contains(c)))
        sb.Append(c);

var targetCharset = sb.ToString();

var rnd = new Random();
char _randomSelect(string charset) => charset[rnd.Next(charset.Length)];

foreach(var _ in Enumerable.Range(0,repeat))
    Console.WriteLine(new string(Enumerable.Range(0, length).Select(_ => _randomSelect(targetCharset)).ToArray()));


