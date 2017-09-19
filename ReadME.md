#Read Me
## Thinking of the process

For this text merge task. the logic is to build a dictionary to store all every text and its responde most match string. Then we loop until the dictionary lennth to 1, find the most match in it , merge remove the two text, insert the merged text to the dictionary and update it. Until its length to 1, we return the last one text  
## Data Structure
A class TextDistance is created to mark how on string can match the other string. Property "Text" is the target match text. Property "Distance" is how much charters it match the original one. 
```
public class TextDistance
{
    {        
        public string Text { get; set; }
        public int Distance { get; set; }
    }
}
```
## Flow 

## Code Structure

### Interface
#### 1. ITextMatcher
This is the interface for all the algrithem, for uppper level to call.
```
 public interface ITextMatcher
    {
        string MatchText(List<string> textFrags);
    }
```
#### 2. IStringHandler
This is the string processes which are indenpendent to the algrithem, CalculateMatchDistance is to caculate the distance with 2 strings. ConcatText is to merge the string depending on the distance.
```
public interface IStringHandler
{
    int CalculateMatchDistance(string firstStr, string secondStr);
    string ConcatText(string firstStr, string secondStr, int length);
}
```
### Class
#### 1. TextMatcher : ITextMatcher
This class implement ITextMatcher which contains all the logic of text list and dictionary match, merge, update logic.
#### 2. StringHandler : IStringHandler
This class implement IStringHandler which contains the logic of calculation and merge.

#### 3. Program
The Main entry point, but we do not use it, we use Unit test to show the result.

## Unit Test
FOr this project, we do not min file IO, so I directly use the Unit test for run all the cases.
#### MatchTextUnitTest
We just need to run all unit test in this file, it contains all the test case I can get.