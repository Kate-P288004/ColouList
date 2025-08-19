
# ColourList – Improved Version

A simple WinForms app that manages a list of colours. This update makes the code modular, adds hex codes, and fixes build issues. It’s written for **C# 7.3** so it works on older Visual Studio targets.

---

## What’s improved

Modularised code
  - Added'Colour' model (`Name`, `Hex`).
  - Added 'ColourRepository' with seed data and helper methods (Add/Update/Delete/GetAll).
Hex codes for each colour
  - Seed list now includes correct '#RRGGBB' values.
  - You can add or edit colours with a hex value.
Cleaner UI output
  - List shows 'Name (#HEX)'.
  - Selecting an item fills the input fields.
Optional Hex box + live preview
  - 'textBoxHex' lets you type a hex like '#008080'.
  - 'panelPreview' shows the colour instantly.
Error handling & validation
  - Duplicate name checks (case-insensitive).
  - Validates hex format '#RRGGBB' and shows messages in 'labelErrorMsg'.
Build errors cleaned
  - Removed newer C# syntax; all code is C# 7.3–safe.

---

## Files you’ll see

```
MyLists/
├─ Colour.cs                 // model
├─ ColourRepository.cs       // seed data + helpers
├─ FormLists.cs              // form logic (uses model + repo)
├─ FormLists.Designer.cs     // Designer (includes textBoxHex + panelPreview)
├─ FormLists.resx
├─ Program.cs
└─ App.config
