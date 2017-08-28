<img src="Wintomaton.png" width="20" height="20"> Wintomaton
=============

This application is an engine to help you automate your workflow in Windows using C# as the underlying scripting engine.  In particular, it allows you to assign behavior to various mouse and keyboard combinations.  Broadly, it allows you to react to various events in Windows by performing some action.

It is essentially similar to an application such as AutoHotkey, with a major difference being that the scripting language is a real language, namely C#, with access to the entire .NET framework.  Because it's not a proprietary DSL your scripts are more powerful and easier to write, since you can use a proper IDE to get intellisense.

To get started, you just need to start the application (`Wintomaton.exe`) and pass as its argument the path to a C# script.

Keyboard
-------------
There are a few different ways you can assign behavior based on what you do on the keyboard.

* **HotKey**  
  Leverages the HotKey mechanism in Windows, allowing you to execute actions based on a KeyCode and modifiers such as control, alt, etc.  This is the optimal solution if feasible, as it does not require the use of a keyboard hook.  As you might expect, the specified KeyCode, when pressed, initiates the action, so at that time the required modifiers must be depressed.
* **Shortcut**  
  Similar to a HotKey, but more flexible as the modifiers can be any key on the keyboard.  Shortcuts require the use of a keyboard hook.
* **KeySequence**  
  Allows you to take an action after a specific sequence of keys has been typed. This is different from the above in that the modifier keys above must be depressed at the same time when pressing the KeyCode that activates the action.  In contrast, keys in a sequence can be pressed one at a time; the only requirement is that they are pressed in sequence.  This facility is great for replacing strings, for example by replacing a key sequnce with emoji or textmoji, or any other string.
* **KeyStroke**  
  When all of the specified keys (one or more) in a KeyStroke are depressed, an action is taken.  This can be useful, for example, for making extra keys on the keyboard execute custom behavior.  This is similar to, but different from, shortcuts except that there is no "activation" KeyCode, so the keys can be pressed in any order.
* **Key Replacement**  
  Replaces a single KeyCode with another KeyCode.