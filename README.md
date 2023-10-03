

<h1>Database Manager - Maui App</h1>
<p>This project is a <strong>Maui App</strong> application that provides a user interface to manage database tables, including CRUD (Create, Read, Update, Delete) operations.</p>

<h2>Table of Contents</h2>
<ul>
    <li><a href="#features">Features</a></li>
    <li><a href="#installation">Installation</a></li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
</ul>

<h2 id="features">Features</h2>
<ul>
    <li><strong>Database Connection</strong>: Connects to a SQLite database stored locally.</li>
    <li><strong>Dynamic Table Selection</strong>: Allows users to select from available tables.</li>
    <li><strong>CRUD Operations</strong>:</li>
    <ul>
        <li><strong>Create</strong>: Add records to a selected table.</li>
        <li><strong>Read</strong>: View records of a selected table.</li>
        <li><strong>Update</strong>: Modify existing records.</li>
        <li><strong>Delete</strong>: Remove records.</li>
    </ul>
    <li><strong>CRUD Testing</strong>: A feature to test CRUD operations on the selected table.</li>
    <li><strong>Modern UI</strong>: Uses frames, buttons, and lists for an engaging experience.</li>
</ul>

<h2 id="installation">Installation</h2>
<h3>Prerequisites</h3>
<ul>
    <li>Visual Studio 2022 with .NET MAUI workload installed.</li>
    <li>.NET SDK 6.0 or higher.</li>
</ul>
<h3>Steps</h3>
<ol>
    <li><strong>Clone the Repository</strong>:
        <pre><code>git clone https://github.com/yourusername/DatabaseManager-MauiApp.git</code></pre>
    </li>
    <li><strong>Open in Visual Studio</strong>: Navigate to the directory and open <code>DatabaseManager-MauiApp.sln</code>.</li>
    <li><strong>Build and Run</strong>: Press <code>F5</code> to build and run the application.</li>
</ol>

<h2 id="usage">Usage</h2>
<ol>
    <li><strong>Start the Application</strong>: Launch the <code>DatabaseManager-MauiApp</code> from Visual Studio or the compiled executable.</li>
    <li><strong>Select a Table</strong>: Use the dropdown to choose from the available tables.</li>
    <li><strong>Manage Records</strong>:<ul>
        <li>Add a new record using the <code>Add Record</code> button.</li>
        <li>Select a record from the list to update or delete.</li>
        <li>Update the selected record's content and press <code>Update Record</code>.</li>
        <li>Delete the selected record by pressing <code>Delete Record</code>.</li>
    </ul></li>
    <li><strong>Test CRUD Operations</strong>: Use the <code>Test CRUD Operations</code> button to test the CRUD functionalities. It inserts a record, updates it, and finally deletes it. You'll receive messages indicating the success or failure of each step.</li>
</ol>

<h2 id="contributing">Contributing</h2>
<ol>
    <li><strong>Fork the Repository</strong>: On GitHub, navigate to the main page of the repository and click on the 'Fork' button.</li>
    <li><strong>Clone Your Fork</strong>:
        <pre><code>git clone https://github.com/yourusername/DatabaseManager-MauiApp.git</code></pre>
    </li>
    <li><strong>Make Changes</strong>: Implement your feature or bug fix.</li>
    <li><strong>Commit and Push</strong>:
        <pre><code>
git add .
git commit -m "Your detailed commit message"
git push origin your-branch
        </code></pre>
    </li>
    <li><strong>Open a Pull Request</strong>: Navigate to the original repository on GitHub. Click on <code>New Pull Request</code> and select your fork.</li>
</ol>

<h2 id="license">License</h2>
<p>This project is licensed under the MIT License. See <code>LICENSE.md</code> for more details.</p>

</body>
</html>
