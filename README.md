## Eye Tracking Data Analysis
The files in the folder "Sanity Checks" take an output file as we receive it from our experiment, extract the necessary information and display the data on 3 different graphs.
The graphs show the correlation between the two eyes, in those parameters: eye openness, pupil diameter and pupil position on the y axis.
We expect to get a high correlation, since it would indicate the eye tracking component is working well. The R value that is added to the graph helps to evaluate the correlation.

camera_pos.cs is the file from the original experiment we had to modify in order to get the eye tracking output from each participant.
The code segments we added are clearly marked with comments.

### Instructions
1. The eye tracking data output file from the experiment should be called "check.csv". Make sure it is the case.
2. Copy check.csv to the same directory as all of the files in the folder "Sanity Checks".
3. Run change_header.py using command prompt.
4. Run prepare_data.py the same way. This will create seperate CSV files that contain the relevant information for each graph. These files will stay in the same directory, do not move them.
5. Run eye_openness.py, pupil_diameter.py, pupil_position_y.py. This will create the graphs for each of the parameters.
