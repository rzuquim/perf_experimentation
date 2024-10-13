
# Conclusions

## `ToList()` vs `ToArray()` - using net8.0

> Q: When realizing an enumerable is it better use `ToList()` or `ToArray()`

- For small **int** arrays (100 items) it seems it is considerably faster to use `ToList()` (20%)
- For bigger **int** arrays both allocation and execution time are the same
- For classes (regardless of the number of properties) there are no noticeable differences
- For structs (regardless of the number of properties) there are no noticeable differences

