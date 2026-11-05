# LADBS (City of Los Angeles) plumbing plan check resubmittal - pre-submittal checklist

Source: the 22-001 Olympic PC9 run (Mechanical Plan Correction Sheet No. 7, 2026). Items marked "(Jeremy)" can only
be confirmed by Jeremy. Add items as plan checkers ask for new things; keep each line checkable.

## Letter
- C1  Response letter addresses EVERY open item on the latest correction sheet, including each sub-bullet; items the
      city struck through (cleared) are not re-argued.
- C2  The city's note requires "a correction narrative indicating where each correction is addressed on the plans":
      every response names the sheet(s).
- C3  Every number and "revised/added/shown" statement in the letter matches the FINAL set (letter_audit.json clean).
- C4  No [PENDING ...] markers or highlighted placeholder text left in the letter.
- C5  Plan check no. and application no. match the correction sheet; project address correct; letter dated.
- C6  Letter date and the title-block issue row for this resubmittal agree (or the difference is intentional).

## Drawings
- C7  Every change since the last submittal is clouded with the current revision delta, and each cloud has its
      delta tag (assembly_qa.json ready).
- C8  Title-block issue-dates block has a row for this resubmittal on every changed sheet.
- C9  Sheet index matches the sheets in the set (set_assemble.py index: nothing missing, nothing extra).
- C10 Code references on all sheets match the project's code cycle (e.g. 2019 CPC / 2020 LAPC when LADBS Building
      confirmed the project may stay on the 2020 cycle); no UPC/IPC or "current code" leftovers.
- C11 Every scheduled product has its cut sheet in the set; values the city reviewed (pump duty, mixing valve,
      water closet make/model for residual pressure) are on the schedule.
- C12 Calcs that changed carry through to schedules, risers and the letter (calc_reviews clean).

## Supporting documents (include in the package or explain in the letter)
- C13 Plan check extension status: if the plan check has expired or is near expiry, the extension request/modification
      form is filed and fees paid (Jeremy).
- C14 Code-cycle confirmation from LADBS Building (if the project stays on an older cycle) is included.
- C15 SCAR (Sewer Capacity Availability Request) approval when sewage ejectors or new sewer load are involved; if it is
      still pending, the letter says so and gives the request number (Jeremy to confirm status).
- C16 Other approvals the correction sheet asks for (e.g. LA Research Report numbers for seismic gas shutoff valves,
      cut sheets) are attached or cited.

## Package
- C17 One current set and one current letter in the transmittal folder; superseded sets/letters moved out or clearly
      dated (Jeremy decides).
- C18 File names carry project, submittal (PCn) and date.
