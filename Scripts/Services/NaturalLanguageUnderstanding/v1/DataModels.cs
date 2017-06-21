﻿/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using FullSerializer;

namespace IBM.Watson.DeveloperCloud.Services.NaturalLanguageUnderstanding.v1
{
    [fsObject]
    public class AnalysisResults
    {
        /// <summary>
        /// The general concepts referenced or alluded to in the specified content
        /// </summary>
        public ConceptsResult[] concepts { get; set; }
        /// <summary>
        /// The important entities in the specified content
        /// </summary>
        public EntitiesResult[] entities { get; set; }
        /// <summary>
        /// The important keywords in content organized by relevance 
        /// </summary>
        public KeywordsResult[] keywords { get; set; }
        /// <summary>
        /// The hierarchical 5-level taxonomy the content is categorized into 
        /// </summary>
        public CategoriesResult[] categories { get; set; }
        /// <summary>
        /// The anger, disgust, fear, joy, or sadness conveyed by the content 
        /// </summary>
        public EmotionResult emotion { get; set; }
        /// <summary>
        /// The metadata holds author information, publication date and the title of the text/HTML content
        /// </summary>
        public MetadataResult metadata { get; set; }
        /// <summary>
        /// The relationships between entities in the content 
        /// </summary>
        public RelationsResult[] relations { get; set; }
        /// <summary>
        /// The subjects of actions and the objects the actions act upon 
        /// </summary>
        public SemanticRolesResult[] semantic_roles { get; set; }
        /// <summary>
        /// The sentiment of the content
        /// </summary>
        public SentimentResult sentiment { get; set; }
        /// <summary>
        /// Language used to analyze the text 
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// Text that was used in the analysis 
        /// </summary>
        public string analyzed_text { get; set; }
        /// <summary>
        /// URL that was used to retrieve HTML content
        /// </summary>
        public string retrieved_url { get; set; }
        /// <summary>
        /// API usage information for the request
        /// </summary>
        public Usage usage { get; set; }
    }

    [fsObject]
    public class ConceptsResult
    {
        /// <summary>
        /// Name of the concept 
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// Relevance score between 0 and 1. Higher scores indicate greater relevance 
        /// </summary>
        public float relevance { get; set; }
        /// <summary>
        /// Link to the corresponding DBpedia resource
        /// </summary>
        public string dbpedia_resource { get; set; }
    }

    [fsObject]
    public class EntitiesResult
    {
        /// <summary>
        /// Entity type
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// Relevance score from 0 to 1. Higher values indicate greater relevance 
        /// </summary>
        public float relevance { get; set; }
        /// <summary>
        /// How many times the entity was mentioned in the text
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// The name of the entity
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// Emotion analysis results for the entity, enabled with the "emotion" option
        /// </summary>
        public EmotionScores emotion { get; set; }
        /// <summary>
        /// Sentiment analysis results for the entity, enabled with the "sentiment" option
        /// </summary>
        public FeatureSentimentResults sentiment { get; set; }
        /// <summary>
        /// Disambiguation information for the entity
        /// </summary>
        public DisambiguationResult disambiguation { get; set; }
    }

    [fsObject]
    public class KeywordsResult
    {
        /// <summary>
        /// Relevance score from 0 to 1. Higher values indicate greater relevance 
        /// </summary>
        public float relevance { get; set; }
        /// <summary>
        /// The keyword text 
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// Emotion analysis results for the keyword, enabled with the "emotion" option
        /// </summary>
        public EmotionScores emotion { get; set; }
        /// <summary>
        /// Sentiment analysis results for the keyword, enabled with the "sentiment" option
        /// </summary>
        public FeatureSentimentResults sentiment { get; set; }
    }

    [fsObject]
    public class CategoriesResult
    {
        /// <summary>
        /// The path to the category through the taxonomy hierarchy
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// Confidence score for the category classification. Higher values indicate greater confidence
        /// </summary>
        public float score { get; set; }
    }

    [fsObject]
    public class EmotionResult
    {
        /// <summary>
        /// The returned emotion results across the document
        /// </summary>
        public DocumentEmotionResults document { get; set; }
        /// <summary>
        /// The returned emotion results per specified target
        /// </summary>
        public TargetedEmotionResults[] targets { get; set; }
    }

    [fsObject]
    public class MetadataResult
    {
        /// <summary>
        /// The authors of the document
        /// </summary>
        public Author[] authors { get; set; }
        /// <summary>
        /// The publication date in the format ISO 8601
        /// </summary>
        public string publication_date { get; set; }
        /// <summary>
        /// The title of the document
        /// </summary>
        public string title { get; set; }
    }

    [fsObject]
    public class RelationsResult
    {
        /// <summary>
        /// Confidence score for the relation. Higher values indicate greater confidence.
        /// </summary>
        public float score { get; set; }
        /// <summary>
        /// The sentence that contains the relation 
        /// </summary>
        public string sentence { get; set; }
        /// <summary>
        /// The type of the relation 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// The extracted relation objects from the text
        /// </summary>
        public RelationArgument[] arguments { get; set; }
    }

    [fsObject]
    public class SemanticRolesResult
    {
        /// <summary>
        /// Sentence from the source that contains the subject, action, and object 
        /// </summary>
        public string sentence { get; set; }
        /// <summary>
        /// The extracted subject from the sentence
        /// </summary>
        public SemanticRolesSubject subject { get; set; }
        /// <summary>
        /// The extracted action from the sentence
        /// </summary>
        public SemanticRolesAction action { get; set; }
        /// <summary>
        /// The extracted object from the sentence
        /// </summary>
        public SemanticRolesObject _object { get; set; }
    }

    [fsObject]
    public class SentimentResult
    {
        /// <summary>
        /// The document level sentiment 
        /// </summary>
        public DocumentSentimentResults document { get; set; }
        /// <summary>
        /// The targeted sentiment to analyze
        /// </summary>
        public TargetedSentimentResults[] targets { get; set; }
    }

    [fsObject]
    public class Usage
    {
        /// <summary>
        /// Number of features used in the API call
        /// </summary>
        public int features { get; set; }
    }

    [fsObject]
    public class EmotionScores
    {
        /// <summary>
        /// Anger score from 0 to 1. A higher score means that the text is more likely to convey anger
        /// </summary>
        public float anger { get; set; }
        /// <summary>
        /// Disgust score from 0 to 1. A higher score means that the text is more likely to convey disgust
        /// </summary>
        public float disgust { get; set; }
        /// <summary>
        /// Fear score from 0 to 1. A higher score means that the text is more likely to convey fear
        /// </summary>
        public float fear { get; set; }
        /// <summary>
        /// Joy score from 0 to 1. A higher score means that the text is more likely to convey joy
        /// </summary>
        public float joy { get; set; }
        /// <summary>
        /// Sadness score from 0 to 1. A higher score means that the text is more likely to convey sadness
        /// </summary>
        public float sadness { get; set; }
    }

    [fsObject]
    public class FeatureSentimentResults
    {
        /// <summary>
        /// Sentiment score from -1 (negative) to 1 (positive)
        /// </summary>
        public float score { get; set; }
    }

    [fsObject]
    public class DisambiguationResult
    {
        /// <summary>
        /// Common entity name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Link to the corresponding DBpedia resource
        /// </summary>
        public string dbpedia_resource { get; set; }
        /// <summary>
        /// Entity subtype information
        /// </summary>
        public string subtype { get; set; }
    }

    [fsObject]
    public class DocumentEmotionResults
    {
        /// <summary>
        /// An object containing the emotion results for the document
        /// </summary>
        public EmotionScores emotion { get; set; }
    }

    [fsObject]
    public class TargetedEmotionResults
    {
        /// <summary>
        /// Targeted text
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// An object containing the emotion results for the target
        /// </summary>
        public EmotionScores emotion { get; set; }
     }

    [fsObject]
    public class Author
    {
        /// <summary>
        /// Name of the author
        /// </summary>
        public string name { get; set; }
    }

    [fsObject]
    public class RelationArgument
    {
        public RelationEntity[] entities { get; set; }
        /// <summary>
        /// Text that corresponds to the argument
        /// </summary>
        public string text { get; set; }
    }

    [fsObject]
    public class SemanticRolesSubject
    {
        /// <summary>
        /// Text that corresponds to the subject role
        /// </summary>
        public string text { get; set; }
        public SemanticRolesEntity[] entities { get; set; }
        public SemanticRolesKeyword[] keywords { get; set; }
    }

    [fsObject]
    public class SemanticRolesAction
    {
        /// <summary>
        /// Analyzed text that corresponds to the action
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// normalized version of the action
        /// </summary>
        public string normalized { get; set; }
        public SemanticRolesVerb verb { get; set; }
    }

    [fsObject]
    public class SemanticRolesObject
    {
        /// <summary>
        /// Object text
        /// </summary>
        public string text { get; set; }
        public SemanticRolesKeyword[] keywords { get; set; }
    }

    [fsObject]
    public class DocumentSentimentResults
    {
        /// <summary>
        /// Sentiment score from -1 (negative) to 1 (positive)
        /// </summary>
        public float score { get; set; }
    }

    [fsObject]
    public class TargetedSentimentResults
    {
        /// <summary>
        /// Targeted text
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// Sentiment score from -1 (negative) to 1 (positive)
        /// </summary>
        public float score { get; set; }
    }

    [fsObject]
    public class RelationEntity
    {
        /// <summary>
        /// Text that corresponds to the entity
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// Entity type
        /// </summary>
        public string type { get; set; }
    }

    [fsObject]
    public class SemanticRolesEntity
    {
        /// <summary>
        /// Entity type
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// The entity text
        /// </summary>
        public string text { get; set; }
    }

    [fsObject]
    public class SemanticRolesKeyword
    {
        /// <summary>
        /// The keyword text
        /// </summary>
        public string text { get; set; }
    }

    [fsObject]
    public class SemanticRolesVerb
    {
        /// <summary>
        /// The keyword text
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// Verb tense
        /// </summary>
        public string tense { get; set; }
    }

    [fsObject]
    public class ListModelsResults
    {
        public CustomModel[] models { get; set; }
    }

    [fsObject]
    public class CustomModel 
    {
        /// <summary>
        /// Shows as available if the model is ready for use
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Unique model ID
        /// </summary>
        public string model_id { get; set; }
        /// <summary>
        /// ISO 639-1 code indicating the language of the model
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// Model description
        /// </summary>
        public string description { get; set; }
    }

    [fsObject]
    public class DeletedResponse
    {
        public string deleted { get; set; }
    }

    #region Version
    /// <summary>
    /// The Discovery version.
    /// </summary>
    public class NaturalLanguageUnderstandingVersion
    {
        /// <summary>
        /// The version.
        /// </summary>
        public const string Version = "2017-02-27";
    }
    #endregion
}
