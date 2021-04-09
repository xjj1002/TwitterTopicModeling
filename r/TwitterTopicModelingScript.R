#https://www.kaggle.com/rashmi11/twitter-sentiment-analysis
#https://rstudio-pubs-static.s3.amazonaws.com/265713_cbef910aee7642dc8b62996e38d2825d.html
#https://towardsdatascience.com/beginners-guide-to-lda-topic-modelling-with-r-e57a5a8e7a25
#https://cran.r-project.org/web/packages/textmineR/textmineR.pdf


#Term Frequency Script in Descending Order

#install.packages('dplyr')
#install.packages("magrittr")
#install.packages('tm',dependencies = TRUE)
#install.packages("tidyr")
packages <- c("magrittr", "dplyr", "tm", "tidyr")
install.packages(setdiff(packages, rownames(installed.packages())))  
library(tidyr)
library(tidyselect)
library(dplyr)
library(tm)

args = commandArgs(trailingOnly = TRUE)
#Importing Error
#this will need changed to correct path
data <- read.csv(args[1])
data<-data[,-c(2,4,5,6)]

#Data Processing 
data <- data[sample(nrow(data)),]
colnames(data)<-c("id","text")
data$id<-factor(data$id)
tweetInfo <- data[which(!grepl("[^\x01-\x7F]+", data$text)),]

#Creating the corpus
tweetCorpus<-VCorpus(VectorSource(tweetInfo$text))
print(tweetCorpus)

#Trimming the Data
NumPunct <- function(x) gsub("[^[:alpha:][:space:]]*","",x) #remove things other than english & space
URLremove <- function(x) gsub("http[^[:space:]]*","",x) #remove the URL
cleantweet <- tm_map(tweetCorpus,content_transformer(URLremove))
cleantweet <- tm_map(cleantweet, content_transformer(NumPunct))                 
cleantweet <- tm_map(cleantweet, removeWords, stopwords())
cleantweet <- tm_map(cleantweet, removeWords, letters)
cleantweet <- tm_map(cleantweet, removePunctuation)
cleantweet <- tm_map(cleantweet, removeNumbers)
cleantweet <- tm_map(cleantweet, stripWhitespace)
cleantweet <- tm_map(cleantweet, content_transformer(tolower))

#Create DTM
tweet_dtm <- DocumentTermMatrix(cleantweet)
#tweet_dtm #(Shows DTM info if we need)
#tweet_tdm <- TermDocumentMatrix(cleantweet)
termsums <- colSums(as.matrix(tweet_dtm), dims=1)
df <- data.frame(termsums)
dfSorted <- df[order(-df$termsums), ,drop = FALSE]
#dfSorted #Output of the list sorted

#Write to a CSV
write.csv(dfSorted, file=args[2])

