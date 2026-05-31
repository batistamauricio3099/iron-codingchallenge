# OldPhonePad — Iron Software Coding Challenge

A C# implementation of an old phone keypad text decoder.

## The Problem

Old mobile phones used a keypad where each number key mapped to multiple letters.
Pressing a key multiple times cycles through its letters. A space in the input means
you paused before pressing the same key again. `*` is backspace. `#` sends the message.

```
1 → &'(    2 → ABC    3 → DEF
4 → GHI    5 → JKL    6 → MNO
7 → PQRS   8 → TUV    9 → WXYZ
0 → (space)
```

## Examples

| Input | Output |
|---|---|
| `33#` | `E` |
| `227*#` | `B` |
| `4433555 555666#` | `HELLO` |
| `8 88777444666*664#` | `TURING` |

## How It Works

The method reads the input one character at a time and tracks which key is being pressed
and how many times in a row. When the key changes, a space is found, or `*` or `#` appear,
it resolves the accumulated presses into a letter using the keymap.

- **Same key repeated** → cycles through the letters (`222` → `C`)
- **Space** → commits the current letter, allows same key to be pressed again
- **`*`** → commits current letter then deletes the last character in the result
- **`#`** → commits current letter and returns the final string
- **Wrap-around** → if presses exceed the letter count, it cycles back (`2222` → `A`)

## Project Structure

```
iron-codingchallenge/
├── OldPhonePad.sln
├── src/
│   └── OldPhonePad/
│       └── OldPhonePad.cs        # Implementation
└── tests/
    └── OldPhonePad.Tests/
        └── OldPhonePadTests.cs   # 26 unit tests
```

## Running the Tests

```bash
dotnet test
```

All 26 tests should pass.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
