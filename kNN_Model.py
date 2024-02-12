import pandas as pd
import sqlalchemy as db
import numpy as np 
from sklearn.neighbors import NearestNeighbors
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler
import pickle



# Connection settings
server_name = "LAPTOP-STO5B2IC"
database_name = "FilmFolio"
trusted_connection = "yes"  # Use "yes" for Windows Authentication, "no" for SQL Server Authentication

# Create connection string
conn_str = (
    f"mssql+pyodbc://{server_name}/{database_name}?driver=SQL+Server&trusted_connection={trusted_connection}"
)

# Create SQLAlchemy engine
engine = db.create_engine(conn_str)

# Test the connection with a simple query
'''
try:
    result = pd.read_sql_query("SELECT 1 as result", con=engine)
except Exception as e:
    print(f"Connection error: {e}")
'''

result_test = pd.read_sql("SELECT TOP 1 f.Id, m.MovieName, m.Director, m.ReleaseYear, m.MovieGenreID, m.AverageRating FROM FavoriteList f FULL OUTER JOIN Movies m ON f.MovieID = m.ID ORDER BY m.AverageRating DESC" , con=engine)

selected_columns_test= result_test[[ 'MovieGenreID', 'ReleaseYear', 'AverageRating']]
selected_columns_int_test = result_test[[ 'MovieGenreID', 'ReleaseYear', 'AverageRating']].astype(int)
test_data = selected_columns_int_test.values
test_genre = result_test[[ 'MovieGenreID']].astype(int)
#print(test_genre)



result_train = pd.read_sql("SELECT * FROM  Movies" , con=engine)

selected_columns_train = result_train[['MovieGenreId', 'ReleaseYear', 'AverageRating']] 
selected_columns_int_train = result_train[[ 'MovieGenreId', 'ReleaseYear', 'AverageRating']].astype(int)
train_data = selected_columns_int_train.values
length = train_data.shape[0]
train_label = np.zeros(length)
# Initialize train_label as an array of strings, with the default value as 'NO'
train_label = np.full(length, 'NO', dtype=object)

# Iterate through each row in test_genre
for index, row in selected_columns_int_train.iterrows():
    genre_id = row['MovieGenreId']
    release_year = row['ReleaseYear']
    average_rating = row['AverageRating']
    # Find matching rows in train_data based on MovieGenreID

    matching_indices = np.where(
        (train_data[:, 0] == genre_id) |
        (train_data[:, 0] == genre_id - 1) |
        (train_data[:, 0] == genre_id + 1) |
        (train_data[:, 1] == release_year)|
        (train_data[:, 1] == release_year +10) |
        (train_data[:, 1] == release_year -10)|
        (train_data[:, 2] == average_rating )|
        (train_data[:, 2] == average_rating +0.5)|
        (train_data[:, 2] == average_rating -0.5)
    )[0]
    
    # Update train_label at those indices with 'YES'
    train_label[matching_indices] = 'YES'

# Now train_label is updated based on the matching MovieGenreID
"""print(train_label)
print(train_data)
print(test_data)"""
"""# Specify the path to your CSV file
file_path = 'C:/Users/Administrator/Desktop/Kitap1 (1).xlsx'

# Read the CSV file
df = pd.read_excel(file_path)"""



X = train_data
y = train_label

# Normalize the feature matrix
scaler = StandardScaler()
X_scaled = scaler.fit_transform(X)

# Split the dataset into training and testing sets
X_train, X_test, y_train, y_test = train_test_split(X_scaled, y, test_size=0.2, random_state=42)

# Initialize the KNN model
knn = NearestNeighbors(n_neighbors=3)

# Train the KNN model
knn.fit(X_train, y_train)

# Let's assume user_preferences is a numpy array representing the user's preferences. 
# It should have the same number of features as your training data.
# For example, if a user prefers a movie genre ID of 5, a release year around 2010, 
# and an average rating of 7, it might look something like this:

result_user_preferences = pd.read_sql("SELECT f.Id, m.MovieGenreId, m.ReleaseYear, m.AverageRating FROM  FavoriteList f INNER JOIN Movies m ON f.MovieId = m.Id ORDER BY f.Id DESC" , con=engine)
user_preferences = result_user_preferences[['MovieGenreId', 'ReleaseYear', 'AverageRating']]

# Normalize the user preferences
user_preferences_scaled = scaler.transform(user_preferences)

# Use the KNN model to find the nearest neighbors
# The function kneighbors returns distances and indices of the neighbors
distances, indices = knn.kneighbors(user_preferences_scaled)

# Retrieve the details of the recommended movies
# 'indices' will give you the row numbers of the similar movies in your original dataset
recommended_movies_indices = indices[0]

# Iterate over the indices to print movie names and image URLs
for index in recommended_movies_indices:
    movie = result_train.iloc[index]
    movie_name = movie['MovieName']  # Replace 'MovieName' with the actual column name for movie names
    image_url = movie['ImageURL']    # Replace 'ImageUrl' with the actual column name for image URLs

    
    print(f"Movie Name: {movie_name}")
    print( f"Image URL: {image_url}")