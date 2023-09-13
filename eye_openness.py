# import pandas as pd
# import matplotlib.pyplot as plt
#
# # Read the CSV file into a DataFrame
# df = pd.read_csv('eye_openness_data.csv')
#
# # Remove the lines containing 'NEW SCENE'
# df = df[pd.notna(df['Frame'])]
#
# # Plotting the data
# plt.scatter(df['Frame'], df['Left Eye openness'], c='blue', label='Left Eye')
# plt.scatter(df['Frame'], df['Right Eye Openness'], c='red', label='Right Eye')
# plt.xlabel('Frame')
# plt.ylabel('Eye Openness')
# plt.title('Eye Openness Comparison')
# plt.legend()
# plt.show()

# scatter plot
import pandas as pd
import matplotlib.pyplot as plt
from scipy.stats import pearsonr

# Load the data from the CSV file
data = pd.read_csv('eye_openness_data.csv')

# Filter out negative values
data = data[data['Left Eye openness'] >= 0]
data = data[data['Right Eye Openness'] >= 0]

# Calculate the correlation coefficient (R-value)
correlation, _ = pearsonr(data['Right Eye Openness'], data['Left Eye openness'])

# Create the scatter plot
plt.figure(figsize=(8, 6))
plt.scatter(data['Right Eye Openness'], data['Left Eye openness'], marker='o', s=40, alpha=0.7)
plt.xlabel('Right Eye openness')
plt.ylabel('Left Eye openness')
plt.title(f'Correlation between Left and Right Eye Openness (R = {correlation:.2f})')
plt.grid(True)
plt.show()
