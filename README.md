# urfu_basicprogramming
https://ulearn.me/course/basicprogramming

## Line endings and editor settings

This repository enforces consistent line endings and editor settings with the following files:

- `.gitattributes` — controls line ending normalization (`eol=lf` for code files, `eol=crlf` for Windows scripts).
- `.editorconfig` — recommended editor rules (end_of_line, indentation, whitespace trimming).
- A GitHub Action (`.github/workflows/check-line-endings.yml`) validates the repository is normalized on PRs and pushes.

If you are a contributor:
- Please set your local Git setting to not automatically convert line endings if possible: `git config --global core.autocrlf false`.
- After pulling these changes, run `git add --renormalize .` then commit/resubmit if your tools changed file endings.
