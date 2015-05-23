The Standard Assets folder
==========================

Scripts in here are always compiled first. Scripts are output to either Assembly-CSharp-firstpass, Assembly-UnityScript-firstpass, or Assembly-Boo-firstpass, depending on the language. See http://docs.unity3d.com/Documentation/Manual/ScriptCompileOrderFolders.html

Scripts inside the Standard Assets folder will be compiled earlier than your other scripts. So, placing scripts in Standard Assets is one way for C# scripts to be able to access .js scripts or vice-versa. 