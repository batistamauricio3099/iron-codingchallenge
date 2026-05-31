# Test Summary Report — OldPhonePad

## Overview

| | |
|---|---|
| **Framework** | xUnit (.NET 8) |
| **Total Tests** | 26 |
| **Passed** | 26 |
| **Failed** | 0 |
| **Pass Rate** | 100% |
| **Execution Time** | ~2.7s |

---

## Testing Methodology

Tests were written using the **Arrange-Act-Assert** pattern and organized into groups,
each targeting a specific behavior of the `OldPhonePad()` method.

I used two xUnit attributes:
- `[Fact]` — for single, self-contained test cases
- `[Theory] + [InlineData]` — for parameterized tests where the same behavior is verified
  with multiple inputs (e.g., testing all keys, or all wrap-around scenarios)

Tests were written **before and alongside** the implementation, not after. Two tests
failed during development and revealed real issues — one in the test itself, one in the
code. Both are documented in [TESTING_NOTES.md](TESTING_NOTES.md).

---

## Test Categories

### 1. Specification Examples (4 tests)
Tests taken directly from the challenge PDF. These are the acceptance criteria.

| Input | Expected | Test |
|---|---|---|
| `33#` | `E` | `OldPhonePad_PdfExamples` |
| `227*#` | `B` | `OldPhonePad_PdfExamples` |
| `4433555 555666#` | `HELLO` | `OldPhonePad_PdfExamples` |
| `8 88777444666*664#` | `TURING` | `OldPhonePad_PdfExamples` |

### 2. Core Key Behavior (7 tests)
Verifies that individual keys resolve correctly.

| Scenario | Input | Expected |
|---|---|---|
| Single press — first letter | `2#` | `A` |
| Single press — first letter | `3#` | `D` |
| Single press — first letter | `9#` | `W` |
| All keys pressed once | `2345678901#` | `ADGJMPTW &` |
| Key 1 first special char | `1#` | `&` |
| Key 1 second special char | `11#` | `'` |
| Key 1 third special char | `111#` | `(` |

### 3. Cycling & Wrap-around (3 tests)
Verifies that pressing a key more times than it has letters wraps back to the first letter.

| Scenario | Input | Expected |
|---|---|---|
| 4 presses on 4-letter key | `7777#` | `S` |
| 5 presses on 4-letter key (wrap) | `77777#` | `P` |
| 4 presses on 3-letter key (wrap) | `2222#` | `A` |

### 4. Space Separator (2 tests)
Verifies that a space allows the same key to be pressed again for a new character.

| Scenario | Input | Expected |
|---|---|---|
| Space separates same key | `2 2#` | `AA` |
| Key 0 produces a space character | `2022#` | `A B` |

### 5. Backspace Behavior (5 tests)
Verifies all backspace scenarios, including edge cases.

| Scenario | Input | Expected |
|---|---|---|
| Backspace removes last letter | `227*#` | `B` |
| Backspace on empty — no crash | `*#` | `` |
| Multiple backspaces | `222**#` | `` |
| More backspaces than characters | `222 33***#` | `` |
| Backspace deletes after space commit | `22 33*2#` | `BA` |
| Backspace deletes space from key 0 | `20*2#` | `AA` |

### 6. Defensive / Edge Cases (5 tests)
Verifies behavior for invalid or unusual inputs.

| Scenario | Input | Expected |
|---|---|---|
| Null input | `null` | `ArgumentNullException` |
| Empty string | `""` | `` |
| Only `#` | `#` | `` |
| Only spaces | `"   #"` | `` |
| Backspace empties result then continues | `22 2**2#` | `A` |

---

## Quality Metrics

These are the metrics I defined to evaluate the completeness of the test suite:

### 1. Specification Coverage
> *Are all examples from the challenge requirements tested?*

**4 / 4 — 100%**
Every example from the PDF is an explicit test case.

### 2. Feature Coverage
> *Is every distinct behavior of the method tested at least once?*

| Feature | Covered |
|---|---|
| Key press → letter | ✅ |
| Multiple presses → cycling | ✅ |
| Wrap-around | ✅ |
| Space as separator | ✅ |
| Key 0 → space character | ✅ |
| Backspace (`*`) | ✅ |
| Send (`#`) | ✅ |
| Special characters (key 1) | ✅ |

**8 / 8 — 100%**

### 3. Edge Case Coverage
> *Are failure-prone or boundary inputs handled?*

| Edge Case | Covered |
|---|---|
| Null input | ✅ |
| Empty string | ✅ |
| Input with no letters | ✅ |
| Backspace on empty result | ✅ |
| More backspaces than characters | ✅ |
| Wrap-around cycling | ✅ |

**6 / 6 — 100%**

### 4. Interaction Coverage
> *Are combinations of features tested together, not just in isolation?*

| Combination | Test |
|---|---|
| Space + backspace | `OldPhonePad_BackspaceDeletesLetterCommittedBySpace` |
| Key 0 (space) + backspace | `OldPhonePad_BackspaceDeletesSpaceFromKey0` |
| Multiple backspaces across word boundary | `OldPhonePad_BackspaceDeletesUntilEmpty` |

**3 interaction tests included.**

---

## How to Run

```bash
dotnet test
```

Expected output:
```
Total tests: 26
Passed: 26
Failed: 0
```
