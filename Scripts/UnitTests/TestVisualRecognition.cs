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

using System.Collections;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;
using System.IO;
using UnityEngine;
using System;
using IBM.Watson.DeveloperCloud.Services.VisualRecognition.v3;

namespace IBM.Watson.DeveloperCloud.UnitTests
{
    public class TestVisualRecognition : UnitTest
    {
        VisualRecognition m_VisualRecognition = new VisualRecognition();
        bool m_TrainClasifierTested = false;
        bool m_GetClassifiersTested = false;
        bool m_FindClassifierTested = false;
        bool m_GetClassifierTested = false;
        bool m_ClassifyGETTested = false;
        bool m_ClassifyPOSTTested = false;
        bool m_DetectFacesGETTested = false;
        bool m_DetectFacesPOSTTested = false;
        bool m_RecognizeTextGETTested = false;
        bool m_RecognizeTextPOSTTested = false;
        bool m_DeleteTested = false;
        
        bool m_TrainClassifier = false;
        bool m_IsClassifierReady = false;
        string m_ClassifierId = null;
        string m_ClassifierName = "unity-integration-test-classifier-giraffe";
        private string m_ImageURL = "https://c2.staticflickr.com/2/1226/1173659064_8810a06fef_b.jpg";   //  giraffe image
        private string m_ImageFaceURL = "https://upload.wikimedia.org/wikipedia/commons/e/e9/Official_portrait_of_Barack_Obama.jpg";    //  Obama image
        private string m_ImageTextURL = "http://i.stack.imgur.com/ZS6nH.png";   //  image with text

        public override IEnumerator RunTest()
        {
            //  test get classifiers
            Log.Debug("TestVisualRecognition", "Getting all classifiers!");
            m_VisualRecognition.GetClassifiers(OnGetClassifiers);
            while(!m_GetClassifiersTested)
                yield return null;
            
            //  test find classifier
            Log.Debug("TestVisualRecognition", "Finding classifier {0}!", m_ClassifierName);
            m_VisualRecognition.FindClassifier(OnFindClassifier, m_ClassifierName);
            while(!m_FindClassifierTested)
                yield return null;
            
            if(m_TrainClassifier)
            {
                //  test train classifier
                Log.Debug("TestVisualRecognition", "Training classifier!");
                string m_positiveExamplesPath = Application.dataPath + "/Watson/Examples/ServiceExamples/TestData/visual-recognition-classifiers/giraffe_positive_examples.zip";
                string m_negativeExamplesPath = Application.dataPath + "/Watson/Examples/ServiceExamples/TestData/visual-recognition-classifiers/negative_examples.zip";
                Test(m_VisualRecognition.TrainClassifier(OnTrainClassifier, m_ClassifierName, "giraffe", m_positiveExamplesPath, m_negativeExamplesPath));
                while(!m_TrainClasifierTested)
                    yield return null;

                Log.Debug("TestVisualRecognition", "Checking classifier {0} status!", m_ClassifierId);
                CheckClassifierStatus(OnCheckClassifierStatus);
                while(!m_IsClassifierReady)
                    yield return null;
            }

            if(!string.IsNullOrEmpty(m_ClassifierId))
            {
                //  test get classifier
                Log.Debug("TestVisualRecognition", "Getting classifier {0}!", m_ClassifierId);
                m_VisualRecognition.GetClassifier(OnGetClassifier, m_ClassifierId);
                while(!m_GetClassifierTested)
                    yield return null;
                
                string[] m_owners = {"IBM", "me"};
                string[] m_classifierIds = {"default", m_ClassifierId};
                
                //  test classify image get
                Log.Debug("TestVisualRecognition", "Classifying image using GET!");
                m_VisualRecognition.Classify(m_ImageURL, OnClassifyGet, m_owners, m_classifierIds);
                while(!m_ClassifyGETTested)
                    yield return null;
                
                //  test classify image post
                Log.Debug("TestVisualRecognition", "Classifying image using POST!");
                string m_classifyImagePath = Application.dataPath + "/Watson/Examples/ServiceExamples/TestData/visual-recognition-classifiers/giraffe_to_classify.jpg";
                m_VisualRecognition.Classify(OnClassifyPost, m_classifyImagePath, m_owners, m_classifierIds);
                while(!m_ClassifyPOSTTested)
                    yield return null;

            }

            //  test detect faces get
            Log.Debug("TestVisualRecognition", "Detecting face image using GET!");
            m_VisualRecognition.DetectFaces(m_ImageFaceURL, OnDetectFacesGet);
            while(!m_DetectFacesGETTested)
                yield return null;

            //  test detect faces post
            Log.Debug("TestVisualRecognition", "Detecting face image using POST!");
            string m_detectFaceImagePath = Application.dataPath + "/Watson/Examples/ServiceExamples/TestData/visual-recognition-classifiers/obama.jpg";
            m_VisualRecognition.DetectFaces(OnDetectFacesPost, m_detectFaceImagePath);
            while(!m_DetectFacesPOSTTested)
                yield return null;

            //  test recognize text get
            Log.Debug("TestVisualRecognition", "Recognizing text image using GET!");
            m_VisualRecognition.RecognizeText(m_ImageTextURL, OnRecognizeTextGet);
            while(!m_RecognizeTextGETTested)
                yield return null;

            //  test recognize text post
            Log.Debug("TestVisualRecognition", "Recognizing text image using POST!");
            string m_recognizeTextImagePath = Application.dataPath + "/Watson/Examples/ServiceExamples/TestData/visual-recognition-classifiers/from_platos_apology.png";
            m_VisualRecognition.RecognizeText(OnRecognizeTextPost, m_recognizeTextImagePath);
            while(!m_RecognizeTextPOSTTested)
                yield return null;

            //  test delete classifier
            Log.Debug("TestVisualRecognition", "Deleting classifier {0}!", m_ClassifierId);
            m_VisualRecognition.DeleteClassifier(OnDeleteClassifier, m_ClassifierId);
            while(!m_DeleteTested)
                yield return null;

            yield break;
        }
            
        private void OnFindClassifier(GetClassifiersPerClassifierVerbose classifier, string customData)
        {
            if (classifier != null)
            {
                Log.Status("TestVisualRecognition", "Find Result, Classifier ID: {0}, Status: {1}", classifier.classifier_id, classifier.status);
                if(classifier.status == "ready")
                {
                    m_TrainClassifier = false;
                    m_IsClassifierReady = true;
                    m_ClassifierId = classifier.classifier_id;
                }
                else
                {
                    m_TrainClassifier = false;
                }
            }
            else
            {
                m_TrainClassifier = true;
            }

            m_FindClassifierTested = true;
        }

        private void OnTrainClassifier(GetClassifiersPerClassifierVerbose classifier, string customData)
        {
            Test(classifier != null);
            if (classifier != null)
            {
                Log.Status("TestVisualRecognition", "Classifier ID: {0}, Classifier name: {1}, Status: {2}", classifier.classifier_id, classifier.name, classifier.status);
                //  store classifier id
                m_ClassifierId = classifier.classifier_id;
            }

            m_TrainClasifierTested = true;
        }

        private void OnGetClassifiers (GetClassifiersTopLevelBrief classifiers, string customData)
        {
            Test(classifiers != null);
            if(classifiers != null && classifiers.classifiers.Length > 0)
            {
                Log.Debug("TestVisualRecognition", "{0} classifiers found!", classifiers.classifiers.Length);
//                foreach(GetClassifiersPerClassifierBrief classifier in classifiers.classifiers)
//                {
//                    Log.Debug("TestVisualRecognition", "Classifier: " + classifier.name + ", " + classifier.classifier_id);
//                }
            }
            else
            {
                Log.Debug("TestVisualRecognition", "Failed to get classifiers!");
            }

            m_GetClassifiersTested = true;
        }

        private void OnGetClassifier(GetClassifiersPerClassifierVerbose classifier, string customData)
        {
            Test(classifier != null);
            if(classifier != null)
            {
                Log.Debug("TestVisualRecognition", "Classifier {0} found! Classifier name: {1}", classifier.classifier_id, classifier.name);
            }

            m_GetClassifierTested = true;
        }

        private void OnClassifyGet(ClassifyTopLevelMultiple classify, string customData)
        {
            Test(classify != null);
            if(classify != null)
            {
                Log.Debug("TestVisualRecognition", "ClassifyImage GET images processed: " + classify.images_processed);
                foreach(ClassifyTopLevelSingle image in classify.images)
                {
                    Log.Debug("TestVisualRecognition", "\tClassifyImage GET source_url: " + image.source_url + ", resolved_url: " + image.resolved_url);
                    foreach(ClassifyPerClassifier classifier in image.classifiers)
                    {
                        Log.Debug("TestVisualRecognition", "\t\tClassifyImage GET classifier_id: " + classifier.classifier_id + ", name: " + classifier.name);
                        foreach(ClassResult classResult in classifier.classes)
                            Log.Debug("TestVisualRecognition", "\t\t\tClassifyImage GET class: " + classResult.m_class + ", score: " + classResult.score + ", type_hierarchy: " + classResult.type_hierarchy);
                    }
                }

                m_ClassifyGETTested = true;
            }
            else
            {
                Log.Debug("TestVisualRecognition", "Classification GET failed!");
            }
        }

        private void OnClassifyPost(ClassifyTopLevelMultiple classify, string customData)
        {
            Test(classify != null);
            if(classify != null)
            {
                Log.Debug("TestVisualRecognition", "ClassifyImage POST images processed: " + classify.images_processed);
                foreach(ClassifyTopLevelSingle image in classify.images)
                {
                    Log.Debug("TestVisualRecognition", "\tClassifyImage POST source_url: " + image.source_url + ", resolved_url: " + image.resolved_url);
                    foreach(ClassifyPerClassifier classifier in image.classifiers)
                    {
                        Log.Debug("TestVisualRecognition", "\t\tClassifyImage POST classifier_id: " + classifier.classifier_id + ", name: " + classifier.name);
                        foreach(ClassResult classResult in classifier.classes)
                            Log.Debug("TestVisualRecognition", "\t\t\tClassifyImage POST class: " + classResult.m_class + ", score: " + classResult.score + ", type_hierarchy: " + classResult.type_hierarchy);
                    }
                }

                m_ClassifyPOSTTested = true;
            }
            else
            {
                Log.Debug("TestVisualRecognition", "Classification POST failed!");
            }
        }

        private void OnDetectFacesGet(FacesTopLevelMultiple multipleImages, string customData)
        {
            Test(multipleImages != null);
            if(multipleImages != null)
            {
                Log.Debug("TestVisualRecognition", "DetectFaces GET  images processed: {0}", multipleImages.images_processed);
                foreach(FacesTopLevelSingle faces in multipleImages.images)
                {
                    Log.Debug("TestVisualRecognition", "\tDetectFaces GET  source_url: {0}, resolved_url: {1}", faces.source_url, faces.resolved_url);
                    foreach(OneFaceResult face in faces.faces)
                    {
                        Log.Debug("TestVisualRecognition", "\t\tDetectFaces GET Face location: {0}, {1}, {2}, {3}", face.face_location.left, face.face_location.top, face.face_location.width, face.face_location.height);
                        Log.Debug("TestVisualRecognition", "\t\tDetectFaces GET Gender: {0}, Score: {1}", face.gender.gender, face.gender.score);
                        Log.Debug("TestVisualRecognition", "\t\tDetectFaces GET Age Min: {0}, Age Max: {1}, Score: {2}", face.age.min, face.age.max, face.age.score);
                        Log.Debug("TestVisualRecognition", "\t\tDetectFaces GET Name: {0}, Score: {1}, Type Heiarchy: {2}", face.identity.name, face.identity.score, face.identity.type_hierarchy);
                    }
                }

                m_DetectFacesGETTested = true;
            }
            else
            {
                Log.Debug("ExampleVisualRecognition", "DetectFaces GET Detect faces failed!");
            }
        }

        private void OnDetectFacesPost(FacesTopLevelMultiple multipleImages, string customData)
        {
            Test(multipleImages != null);
            if(multipleImages != null)
            {
                Log.Debug("TestVisualRecognition", "DetectFaces POST  images processed: {0}", multipleImages.images_processed);
                foreach(FacesTopLevelSingle faces in multipleImages.images)
                {
                    Log.Debug("TestVisualRecognition", "\tDetectFaces POST  source_url: {0}, resolved_url: {1}", faces.source_url, faces.resolved_url);
                    foreach(OneFaceResult face in faces.faces)
                    {
                        Log.Debug("TestVisualRecognition", "\t\tDetectFaces POST Face location: {0}, {1}, {2}, {3}", face.face_location.left, face.face_location.top, face.face_location.width, face.face_location.height);
                        Log.Debug("TestVisualRecognition", "\t\tDetectFaces POST Gender: {0}, Score: {1}", face.gender.gender, face.gender.score);
                        Log.Debug("TestVisualRecognition", "\t\tDetectFaces POST Age Min: {0}, Age Max: {1}, Score: {2}", face.age.min, face.age.max, face.age.score);
                        Log.Debug("TestVisualRecognition", "\t\tDetectFaces POST Name: {0}, Score: {1}, Type Heiarchy: {2}", face.identity.name, face.identity.score, face.identity.type_hierarchy);
                    }
                }

                m_DetectFacesPOSTTested = true;
            }
            else
            {
                Log.Debug("ExampleVisualRecognition", "DetectFaces POST Detect faces failed!");
            }
        }

        private void OnRecognizeTextGet(TextRecogTopLevelMultiple multipleImages, string customData)
        {
            Test(multipleImages != null);
            if(multipleImages != null)
            {
                Log.Debug("TestVisualRecognition", "RecognizeText GET images processed: {0}", multipleImages.images_processed);
                foreach(TextRecogTopLevelSingle texts in multipleImages.images)
                {
                    Log.Debug("TestVisualRecognition", "\tRecognizeText GET source_url: {0}, resolved_url: {1}", texts.source_url, texts.resolved_url);
                    Log.Debug("TestVisualRecognition", "\tRecognizeText GET text: {0}", texts.text);
//                    foreach(TextRecogOneWord text in texts.words)
//                    {
//                        Log.Debug("TestVisualRecognition", "\t\tRecognizeText GET text location: {0}, {1}, {2}, {3}", text.location.left, text.location.top, text.location.width, text.location.height);
//                        Log.Debug("TestVisualRecognition", "\t\tRecognizeText GET Line number: {0}", text.line_number);
//                        Log.Debug("TestVisualRecognition", "\t\tRecognizeText GET word: {0}, Score: {1}", text.word, text.score);
//                    }
                }

                m_RecognizeTextGETTested = true;
            }
            else
            {
                Log.Debug("ExampleVisualRecognition", "RecognizeText GET failed!");
            }
        }

        private void OnRecognizeTextPost(TextRecogTopLevelMultiple multipleImages, string customData)
        {
            Test(multipleImages != null);
            if(multipleImages != null)
            {
                Log.Debug("TestVisualRecognition", "RecognizeText POST images processed: {0}", multipleImages.images_processed);
                foreach(TextRecogTopLevelSingle texts in multipleImages.images)
                {
                    Log.Debug("TestVisualRecognition", "\tRecognizeText POST source_url: {0}, resolved_url: {1}", texts.source_url, texts.resolved_url);
                    Log.Debug("TestVisualRecognition", "\tRecognizeText POST text: {0}", texts.text);
//                    foreach(TextRecogOneWord text in texts.words)
//                    {
//                        Log.Debug("TestVisualRecognition", "\t\tRecognizeText POST text location: {0}, {1}, {2}, {3}", text.location.left, text.location.top, text.location.width, text.location.height);
//                        Log.Debug("TestVisualRecognition", "\t\tRecognizeText POST Line number: {0}", text.line_number);
//                        Log.Debug("TestVisualRecognition", "\t\tRecognizeText POST word: {0}, Score: {1}", text.word, text.score);
//                    }
                }

                m_RecognizeTextPOSTTested = true;
            }
            else
            {
                Log.Debug("ExampleVisualRecognition", "RecognizeText POST failed!");
            }
        }

        private void OnDeleteClassifier(bool success, string customData)
        {
            if(success)
            {
                m_VisualRecognition.FindClassifier(OnDeleteClassifierFinal, m_ClassifierName);
            }

            m_DeleteTested = true;
            Test(success);
        }

        private void CheckClassifierStatus(VisualRecognition.OnGetClassifier callback, string customData = default(string))
        {
            if(!m_VisualRecognition.GetClassifier(callback, m_ClassifierId))
                Log.Debug("TestVisualRecognition", "Get classifier failed!");
        }

        private void OnCheckClassifierStatus(GetClassifiersPerClassifierVerbose classifier, string customData)
        {
            Log.Debug("TestVisualRecognition", "classifier {0} is {1}!", classifier.classifier_id, classifier.status);

            if(classifier.status == "unavailable" || classifier.status == "failed")
            {
                Log.Debug("TestVisualRecognition", "Deleting classifier!");
                //  classifier failed - delete!
                if(!m_VisualRecognition.DeleteClassifier(OnCheckClassifierStatusDelete, classifier.classifier_id))
                    Log.Debug("TestVisualRecognition", "Failed to delete classifier {0}!", m_ClassifierId);

            }
            else if(classifier.status == "training")
            {
                CheckClassifierStatus(OnCheckClassifierStatus);
            }
            else if(classifier.status == "ready")
            {
                m_IsClassifierReady = true;
                m_ClassifierId = classifier.classifier_id;
            }
        }

        private void OnCheckClassifierStatusDelete(bool success, string customData)
        {
            if(success)
            {
                //  train classifier again!
                m_TrainClasifierTested = false;

            }
        }

        private void OnDeleteClassifierFinal(GetClassifiersPerClassifierVerbose classifier, string customData)
        {
            if(classifier == null)
            {
                Log.Debug("TestVisualRecognition", "Classifier not found! Delete sucessful!");
            }
            else
            {
                Log.Debug("TestVisualRecognition", "Classifier {0} found! Delete failed!", classifier.name);
            }
        }
    }
}
