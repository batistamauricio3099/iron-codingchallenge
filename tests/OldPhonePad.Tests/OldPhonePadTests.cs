using OldPhonePad;

namespace OldPhonePad.Tests;

public class OldPhonePadTests
{
    // --- Examples from the PDF ---
    [Theory]
    [InlineData("33#", "E")]
    [InlineData("227*#", "B")]
    [InlineData("4433555 555666#", "HELLO")]
    [InlineData("8 88777444666*664#", "TURING")]   // "?????" in the PDF
    public void OldPhonePad_PdfExamples(string input, string expected)
    {
        Assert.Equal(expected, PhonePad.OldPhonePad(input));
    }

    // --- Single key press ---
    [Theory]
    [InlineData("2#", "A")]
    [InlineData("3#", "D")]
    [InlineData("9#", "W")]
    public void OldPhonePad_SingleKeyPress_ReturnsFirstLetter(string input, string expected)
    {
        Assert.Equal(expected, PhonePad.OldPhonePad(input));
    }

    // --- Space separates two presses of the same key ---
    [Fact]
    public void OldPhonePad_SpaceSeparatesSameKey_ReturnsTwoSameLetters()
    {
        Assert.Equal("AA", PhonePad.OldPhonePad("2 2#"));
    }

    // --- Key 0 produces a space character ---
    [Fact]
    public void OldPhonePad_Key0_ProducesSpace()
    {
        Assert.Equal("A B", PhonePad.OldPhonePad("2022#"));  // 2=A, 0=space, 22=B
    }

    // --- Cycling wraps around when presses exceed letter count ---
    [Theory]
    [InlineData("7777#", "S")]   // PQRS: 4 presses -> index 3 -> S
    [InlineData("77777#", "P")]  // PQRS: 5 presses -> wraps back to index 0 -> P
    [InlineData("2222#", "A")]   // ABC:  4 presses -> wraps back to index 0 -> A
    public void OldPhonePad_CyclingWrapsAround(string input, string expected)
    {
        Assert.Equal(expected, PhonePad.OldPhonePad(input));
    }

    // --- Backspace on empty string does not crash ---
    [Fact]
    public void OldPhonePad_BackspaceOnEmpty_ReturnsEmpty()
    {
        Assert.Equal("", PhonePad.OldPhonePad("*#"));
    }

    // --- Multiple backspaces ---
    [Fact]
    public void OldPhonePad_MultipleBackspaces_DeletesCorrectly()
    {
        Assert.Equal("", PhonePad.OldPhonePad("222**#"));  // type C, delete twice
    }

    // --- Only send key ---
    [Fact]
    public void OldPhonePad_OnlySend_ReturnsEmpty()
    {
        Assert.Equal("", PhonePad.OldPhonePad("#"));
    }

    // --- Null input ---
    [Fact]
    public void OldPhonePad_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => PhonePad.OldPhonePad(null!));
    }

    // --- Empty string (no # at all) ---
    [Fact]
    public void OldPhonePad_EmptyString_ReturnsEmpty()
    {
        Assert.Equal("", PhonePad.OldPhonePad(""));
    }

    // --- Only spaces before send ---
    [Fact]
    public void OldPhonePad_OnlySpaces_ReturnsEmpty()
    {
        Assert.Equal("", PhonePad.OldPhonePad("   #"));
    }

    // --- Key 1 special characters ---
    [Theory]
    [InlineData("1#", "&")]
    [InlineData("11#", "'")]
    [InlineData("111#", "(")]
    public void OldPhonePad_Key1_ReturnsSpecialCharacters(string input, string expected)
    {
        Assert.Equal(expected, PhonePad.OldPhonePad(input));
    }

    // --- Backspace deletes until empty then does nothing ---
    [Fact]
    public void OldPhonePad_BackspaceDeletesUntilEmpty()
    {
        Assert.Equal("A", PhonePad.OldPhonePad("22 2**2#"));  // B, B, delete, delete, A -> A
    }

    // --- Backspace deletes a letter committed by a space separator ---
    [Fact]
    public void OldPhonePad_BackspaceDeletesLetterCommittedBySpace()
    {
        Assert.Equal("BA", PhonePad.OldPhonePad("22 33*2#"));  // B, E, backspace E, A -> BA
    }

    // --- More backspaces than characters typed ---
    [Fact]
    public void OldPhonePad_MoreBackspacesThanCharacters_ReturnsEmpty()
    {
        Assert.Equal("", PhonePad.OldPhonePad("222 33***#"));  // C, E, *, *, * -> ""
    }

    // --- Every key used once ---
    [Fact]
    public void OldPhonePad_AllKeys_ReturnFirstLetterOfEach()
    {
        Assert.Equal("ADGJMPTW &", PhonePad.OldPhonePad("2345678901#"));
    }

    // --- Backspace deletes a space produced by key 0 ---
    [Fact]
    public void OldPhonePad_BackspaceDeletesSpaceFromKey0()
    {
        Assert.Equal("AA", PhonePad.OldPhonePad("20*2#"));  // A, space, backspace space, A -> AA
    }
}
