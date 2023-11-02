﻿<h1>UNDAC Action Pipeline</h1>

<p>This is brief summary of current pipeline steps using windows2022 runner.</p>

<h2>Table of Contents</h2>
<ul>
    <li><a href="#steps">Steps</a></li>
    <li><a href="#license">License</a></li>
</ul>

<h2 id="steps">Steps</h2>
<ul>
<li><strong>Building:</strong></li>
<ul>
    <li><strong>Set up job</strong>: Initial step that sets up the working environment, with access variables like <code>GITHUB_TOKEN</code></li>
    <li><strong>Checkout</strong>: Pulls repository and Checkouts on the Specified Branch.</li>
    <li><strong>Setup .NET 7</strong>: Installs the latest version of .NET 7 Framework</li>
  <li><strong>Setup MSBuild</strong>: Based on the <a href="https://blog.taranissoftware.com/building-net-maui-apps-with-github-actions">Taranissoftware</a> <code>dotnet build</code> currently does not support MAUI applications, thus MSBuild setup is also required.</li>
<li><strong>Install MAUI Workloads</strong>: Windows-2022 runner does not have in-built MAUI workloads; thus these need to be downloaded.</li>
<li><strong>Restore Dependencies</strong>: Restores any dependencies that the repository could have before the building.</li>
<li><strong>Build MAUI Windows</strong>: Build the MAUI application with <code>/p:GenerateAppxPackageOnBuild=true</code> flag to generate artifact for artifact upload step.</li>
<li><strong>Upload Windows Artifact</strong>: Download generated artefact if successful (Currently in development)</li>
</ul>
<li><strong>Testing:</strong></li>
  <ul>
    <li>To be completed</li>
  </ul>
 
</ul>



<h2 id="license">License</h2>
<p>This project is licensed under the MIT License. See <code>LICENSE.md</code> for more details.</p>

</body>
</html>