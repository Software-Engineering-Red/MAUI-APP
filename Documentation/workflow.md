## Workflow:
#### When working on an issue
- Move the issue to in progress, and assign to yourself
- Make a feature branch with the naming convention feature/MAUI-APP#<your issue number>, push directly to there, NOT Develop or Master
- Make all commit messages verbose, so the whole team can understand what you have added
- An issue can be closed once the changes have been reviewed and merged in

#### When moving your branch to Develop/Master
- Ensure code is adequently commented and tested (if required)
- When finished, your branch needs to have a Pull Request to move it into Develop, and again from Develop to Master. To avoid merge conflicts when merging your branch to Develop, ensure your branch is up-to-date with it <b>BEFORE</b> you make a Pull Request
- When making a PR from your branch to Develop, or Develop to Master, ensure two people have reviewed your code, and follow up with any comments they have made before continuing. Additionally, click "squash commit" when completing a PR, to keep our repo clean 
- If you break the repo, you fix it so ensure the changes do work on a development branch before merging onto Develop or Master

#### Definition of Done (Kanban):
- All acceptance criteria on issue are met
- All integration tests/Unit tests are passed and/or written if required
- All review comments have been correctly responded to and addressed if required
- Procedure for updating a task: - Any additional information should be added in comments on the issue itself with as much detail as possible available to the assigned programmer