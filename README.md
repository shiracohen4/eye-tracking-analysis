## Eye Tracking Data Analysis
The files in the folder "Sanity Checks" take an output file as we receive it from our experiment, extract the necessary information and display the data on 3 different graphs. 
The graphs show the correlation between the two eyes, in those parameters: eye openness, pupil diameter and pupil position on the y axis. 
We expect to get a high correlation, since it would indicate the eye tracking component is working well. The R value that is added to the graph helps to evaluate the correlation.

The file camera_pos.cs is the file from the original experiment we had to modify in order to get the eye tracking output from each participant.

### Instructions
1. Name the output file "data.csv".
2. Make sure all of the files in this folder are in the same directory as data.csv.
3. Run prepare_data.py. This will create the necessary files for creating the graphs.
4. Run eye_openness.py, pupil_diameter.py, pupil_position_y.py. This will create the graphs for each of the parameters.
