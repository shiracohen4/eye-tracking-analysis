# import pandas as pd
# import matplotlib.pyplot as plt
#
# # Read the CSV file into a DataFrame
# df = pd.read_csv('pupil_position_y_data.csv')
#
# # Remove the lines containing 'NEW SCENE'
# df = df[pd.notna(df['Frame'])]
#
# # Plotting the data
# plt.scatter(df['Frame'], df['Left Pupil Position y'], c='green', label='Left Pupil')
# plt.scatter(df['Frame'], df['Right Pupil Position y'], c='orange', label='Right Pupil')
# plt.xlabel('Frame')
# plt.ylabel('Pupil Position Along Y Axis')
# plt.title('Pupil Y Axis Comparison')
# plt.legend()
# plt.show()


# scatter plot
import pandas as pd
import matplotlib.pyplot as plt
from scipy.stats import pearsonr

# Load the data from the CSV file
data = pd.read_csv('pupil_position_y_data.csv')

# Filter out negative values
data = data[data['Left Pupil Position y'] >= 0]
data = data[data['Right Pupil Position y'] >= 0]

# Calculate the correlation coefficient (R-value)
correlation, _ = pearsonr(data['Right Pupil Position y'], data['Left Pupil Position y'])

# Create the scatter plot
plt.figure(figsize=(8, 6))
plt.scatter(data['Right Pupil Position y'], data['Left Pupil Position y'], marker='o', s=40, alpha=0.7)
plt.xlabel('Right Pupil Position y')
plt.ylabel('Left Pupil Position y')
plt.title(f'Correlation between Left and Right Eye Pupil Positions (R = {correlation:.2f})')
plt.grid(True)
plt.show()

