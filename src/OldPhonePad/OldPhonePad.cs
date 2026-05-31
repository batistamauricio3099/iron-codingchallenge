namespace OldPhonePad;

public static class PhonePad
{
    private static readonly Dictionary<char, string> KeyMap = new()
    {
        { '1', "&'(" },
        { '2', "ABC" },
        { '3', "DEF" },
        { '4', "GHI" },
        { '5', "JKL" },
        { '6', "MNO" },
        { '7', "PQRS" },
        { '8', "TUV" },
        { '9', "WXYZ" },
        { '0', " " },
    };

    public static string OldPhonePad(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var result = new System.Text.StringBuilder();
        char currentKey = '\0';
        int pressCount = 0;

        foreach (char c in input)
        {
            if (c == '#')
            {
                CommitKey(result, currentKey, pressCount);
                break;
            }

            if (c == '*')
            {
                CommitKey(result, currentKey, pressCount);
                currentKey = '\0';
                pressCount = 0;

                if (result.Length > 0)
                    result.Remove(result.Length - 1, 1);

                continue;
            }

            if (c == ' ')
            {
                CommitKey(result, currentKey, pressCount);
                currentKey = '\0';
                pressCount = 0;
                continue;
            }

            if (c == currentKey)
            {
                pressCount++;
            }
            else
            {
                CommitKey(result, currentKey, pressCount);
                currentKey = c;
                pressCount = 1;
            }
        }

        return result.ToString();
    }

    private static void CommitKey(System.Text.StringBuilder result, char key, int presses)
    {
        if (presses == 0 || !KeyMap.TryGetValue(key, out string? letters))
            return;
        result.Append(letters[(presses - 1) % letters.Length]);
    }
}
