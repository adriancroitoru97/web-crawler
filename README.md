# Aragog - the web crawler

A C# application used to find the best fitting job for an user.
It crawls job finder websites and, in fact, the whole internet.


## The idea

The user provides the **location** and the **domain** for the desired job.
**Keywords** can also be provided. The program crawls some job finder websites
and returns a list of most relevant jobs, based on the first two
criteria (location and domain). After that, it searches in every job announce
found and provides a **match percentage** for each job, based on the number of
occurrences of the keywords. It also crawls on Google,
in order to return the website of the employer.

So, after the script is done, the user has a list of the most relevant jobs,
with a percentage for each job, the job announce link,
the employer and it's website.


## License
[Adrian-Valeriu Croitoru](https://github.com/adriancroitoru97/)\
[Tudor Diaconu](https://github.com/tudordiaconu/)\
[Vlad Novetschi]()