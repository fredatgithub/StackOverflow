# Usuage

Step 1.

    dnvm use 1.0.0-rc1-final -runtime clr
    dnu restore
    dnu build
    dnx run
    
Step 2.

* navigate to localhost:5000
* navigate to localhost:5000/env

Step 3. 

* Delete hosting.json
* Restart `dnx run`. 
* Repeat step 2 and notice the difference.