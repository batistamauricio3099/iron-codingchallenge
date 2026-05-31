# How Tests Caught My Mistakes

While building the OldPhonePad solution I wrote a bunch of unit tests to make sure everything worked.
Two of them failed. Here's what happened and what I learned from it.

---

## Case 1 — The bug was in MY test, not the code

I wrote a test to check that pressing key `0` produces a space character.
I used `"20 2#"` as input and expected `"A B"` as output.

It failed:

```
Expected: A B
Actual:   A A
```

I stared at it for a bit and then I realized — the code was right, I was wrong.

`"20 2#"` means: press 2 once (A), press 0 (space), press 2 once (A). That's `"A A"`, not `"A B"`.
If I want a `B`, I need to press 2 twice. The correct input should be `"2022#"`.

I fixed the test and it passed.

The lesson here is that tests can have bugs too. Writing a wrong test and seeing it fail is actually
still useful — it forced me to slow down and think through the input carefully instead of just
assuming I was right.

---

## Case 2 — The code was silently crashing in a confusing way

I wrote a test to check what happens when someone passes `null` as input.
I expected the method to throw an `ArgumentNullException` — a clear error that says
"hey, you passed null, that's not allowed."

It failed:

```
Expected: typeof(System.ArgumentNullException)
Actual:   typeof(System.NullReferenceException)
```

The code was crashing, but with a generic `NullReferenceException` that just says
"something was null somewhere." That's not helpful at all if you're a developer trying to
figure out what went wrong.

I added one line at the top of the method:

```csharp
ArgumentNullException.ThrowIfNull(input);
```

Now if you pass `null`, you get a clear error that tells you exactly what the problem is.

The lesson here is that "it throws an exception" is not the same as "it fails gracefully."
The test made me think about the difference and actually handle the bad input properly.

---

## The bigger takeaway

Both failures taught me something. One caught a mistake in my thinking,
the other caught a mistake in the code. Either way, without the tests I would have
shipped something that either gave wrong results or crashed in a confusing way.

That's pretty much why tests exist.
