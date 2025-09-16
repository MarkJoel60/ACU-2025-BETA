<?xml version="1.0" encoding="UTF-8"?>
<!-- ============================================================= -->
<!--                        Acumatica Inc.                         -->
<!--          Copyright (c) 1994-2011 All rights reserved.         -->
<!--                                                               -->
<!--                                                               -->
<!-- This file and its contents are protected by United States     -->
<!-- and International copyright laws.  Unauthorized reproduction  -->
<!-- and/or distribution of all or any portion of the code         -->
<!-- contained here in is strictly prohibited and will result in   -->
<!-- severe civil and criminal penalties.                          -->
<!-- Any violations of this copyright will be prosecuted       	   -->
<!-- to the fullest extent possible under law.                     -->
<!--                                                               -->
<!-- UNDER NO CIRCUMSTANCES MAY THE SOURCE CODE BE USED IN WHOLE   -->
<!-- OR IN PART, AS THE BASIS FOR CREATING A PRODUCT THAT PROVIDES -->
<!-- THE SAME, OR SUBSTANTIALLY THE SAME, FUNCTIONALITY AS ANY     -->
<!-- ProjectX PRODUCT.                                             -->
<!-- THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.      -->
<!-- ============================================================= -->

 

<!-- =====================================================================
     ENTITY DECLARATIONS FOR DOMAIN SUBSTITUTION
     ===================================================================== -->

<!ENTITY % lcInteractionBase        "lcInteractionBase">
<!ENTITY % lcQuestionBase           "lcQuestionBase">

<!-- =====================================================================
     INTERACTION BASE DEFINITIONS
     ===================================================================== -->
<!ENTITY % lcInteractionBase.content
                       "((%title;)?,
                         (%lcQuestionBase;), 
                         (%fig.cnt;)*)"
>
<!ENTITY % lcInteractionBase.attributes
             "id
                        NMTOKEN
                                  #REQUIRED
              %conref-atts;
              %select-atts;
              %localization-atts;
              outputclass
                        CDATA
                                  #IMPLIED"
>
<!ELEMENT lcInteractionBase    %lcInteractionBase.content;>
<!ATTLIST lcInteractionBase    %lcInteractionBase.attributes;>

<!ENTITY % lcQuestionBase.content
                       "(%ph.cnt;)*"
>
<!ENTITY % lcQuestionBase.attributes
             "%univ-atts;
              outputclass
                        CDATA
                                  #IMPLIED"
>
<!ELEMENT lcQuestionBase    %lcQuestionBase.content;>
<!ATTLIST lcQuestionBase    %lcQuestionBase.attributes;>

<!-- =====================================================================
     CLASS ATTRIBUTES FOR ANCESTRY DECLARATION
     ===================================================================== -->
<!ATTLIST lcInteractionBase %global-atts;
    class CDATA "+ topic/fig learningInteractionBase-d/lcInteractionBase ">
<!ATTLIST lcQuestionBase %global-atts;
    class CDATA "+ topic/p   learningInteractionBase-d/lcQuestionBase ">

<!--============ End of learning interaction base module ==================-->