# word-frequency-calculator

# Assumptions
1. Considered words and letters as a word - @"[A-Za-z]+"
2. All characters othere than letters are ignored
3. All letters are considered lowercase
4. No restriction on file size or number of words
5. No restriction on word length
6. Only <b>*.txt</b> file will be processed
7. Path to file need to passed as parameter from console <b>e.g. D:\lordoftherings.txt</b>
8. By default it lists top 10 words with occurencs but, you can increase or decrease output number of words by passing number to method <b>GetMostFrequentUsedWords(int topNWords = 10)</b>
