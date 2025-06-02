import {
    Card,
    LargeTitle,
    Title2,
    Title3,
    Tree,
    TreeItem,
    TreeItemLayout
} from '@fluentui/react-components';
import { AddSquareRegular, ArrowCircleRightRegular, SubtractSquareRegular } from '@fluentui/react-icons';

import { useStyles } from './styles';
import { useState } from 'react';
import descriptionData from './descriptionData.json';

/**
 * @component DescriptionComponent
 * @description A comprehensive component that displays detailed information about the Internet Bulletin Board System (IBBS).
 * The component is structured with multiple sections including introduction, key features, usage instructions,
 * technical details, and upcoming features. Each section is organized in an interactive tree structure
 * using Fluent UI components for better user experience and navigation.
 * 
 * @section Structure
 * - Introduction and What is IBBS: Basic information cards
 * - Key Features: Interactive tree with user management, content management, and administrative features
 * - How to Use: Step-by-step guide with getting started, browsing, creating content, and interaction instructions
 * - Technical Excellence: Details about architecture, infrastructure, design, and security
 * - Upcoming Features: Planned features categorized by user management, content management, and communication tools
 * 
 * @state {string[]} openItems - Array of tree item IDs that are currently expanded
 * 
 * @function handleOpenChange
 * @param {Object} event - The event object
 * @param {Object} data - The data object containing open state and value
 * @param {boolean} data.open - Whether the tree item should be opened
 * @param {string} data.value - The ID of the tree item being toggled
 * 
 * @example
 * <DescriptionComponent />
 * 
 * @requires @fluentui/react-components
 * @requires @fluentui/react-icons
 * @requires ./styles
 * 
 * @returns {JSX.Element} A structured component displaying IBBS information
 */
function DescriptionComponent() {
    const styles = useStyles();

    const [openItems, setOpenItems] = useState(['tree-item-5', 'tree-item-6', 'tree-item-11', 'tree-item-14']);

    /**
     * Handle the open change event for the tree items
     * @param {Event} _ The event object
     * @param {Object} data The data object containing open state and value
     */
    const handleOpenChange = (_, data) => {
        setOpenItems((curr) =>
            data.open
                ? [...curr, data.value]
                : curr.filter((value) => value !== data.value)
        );
    };

    return (
        <div className={`row ${styles.descriptionContainer}`}>
            <LargeTitle className={styles.titles}>
                {descriptionData.title}
            </LargeTitle>

            <div className="row">
                <div className="col-6 col-sm-6">
                    <Card className={styles.card}>
                        <h2>{descriptionData.introduction.title}</h2>
                        <p>{descriptionData.introduction.content}</p>
                    </Card>
                </div>
                <div className="col-6 col-sm-6">
                    <Card className={styles.card}>
                        <h2>{descriptionData.whatIsIBBS.title}</h2>
                        <p>{descriptionData.whatIsIBBS.content}</p>
                    </Card>
                </div>
            </div>

            <div className="row">
                <div className="col-6 col-sm-6">
                    <Tree
                        aria-label='key-features'
                        className='key-features'
                        openItems={openItems}
                        onOpenChange={handleOpenChange}
                    >
                        <Card className={styles.treeCard}>
                            <TreeItem itemType='branch' value="tree-item-5">
                                <TreeItemLayout
                                    expandIcon={openItems.includes("tree-item-5") ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                >
                                    <Title2 className={styles.titles}>{descriptionData.keyFeatures.title}</Title2>
                                </TreeItemLayout>

                                <Tree>
                                    {descriptionData.keyFeatures.sections.map((section, index) => (
                                        <TreeItem key={index} itemType='branch' value={`tree-item-${index + 1}`}>
                                            <TreeItemLayout
                                                expandIcon={openItems.includes(`tree-item-${index + 1}`) ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                            >
                                                <Title3>{section.title}</Title3>
                                            </TreeItemLayout>
                                            <Tree>
                                                <TreeItem itemType='leaf'>
                                                    <TreeItemLayout>
                                                        <ul>
                                                            {section.items.map((item, itemIndex) => (
                                                                <li key={itemIndex} className={styles.listItem}>
                                                                    <ArrowCircleRightRegular /> {item}
                                                                </li>
                                                            ))}
                                                        </ul>
                                                    </TreeItemLayout>
                                                </TreeItem>
                                            </Tree>
                                        </TreeItem>
                                    ))}
                                </Tree>
                            </TreeItem>
                        </Card>
                    </Tree>
                </div>

                <div className="col-6 col-sm-6">
                    <Tree
                        aria-label='how-to-use'
                        className='how-to-use'
                        openItems={openItems}
                        onOpenChange={handleOpenChange}
                    >
                        <Card className={styles.treeCard}>
                            <TreeItem itemType='branch' value="tree-item-6">
                                <TreeItemLayout
                                    expandIcon={openItems.includes("tree-item-6") ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                >
                                    <Title2 className={styles.titles}>How to Use IBBS</Title2>
                                </TreeItemLayout>

                                <Tree>
                                    <TreeItem itemType='branch' value='tree-item-7'>
                                        <TreeItemLayout
                                            expandIcon={openItems.includes("tree-item-7") ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                        >
                                            <Title3>Getting Started</Title3>
                                        </TreeItemLayout>
                                        <Tree>
                                            <TreeItem itemType='leaf'>
                                                <TreeItemLayout>
                                                    <ul>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Create an account using your email</li>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Complete your profile</li>
                                                    </ul>
                                                </TreeItemLayout>
                                            </TreeItem>
                                        </Tree>
                                    </TreeItem>

                                    <TreeItem itemType='branch' value='tree-item-8'>
                                        <TreeItemLayout
                                            expandIcon={openItems.includes("tree-item-8") ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                        >
                                            <Title3>Browsing Content</Title3>
                                        </TreeItemLayout>
                                        <Tree>
                                            <TreeItem itemType='leaf'>
                                                <TreeItemLayout>
                                                    <ArrowCircleRightRegular />Not much speciality is needed, all the information is in the main page itself.
                                                </TreeItemLayout>
                                            </TreeItem>
                                        </Tree>
                                    </TreeItem>

                                    <TreeItem itemType='branch' value='tree-item-9'>
                                        <TreeItemLayout
                                            expandIcon={openItems.includes("tree-item-9") ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                        >
                                            <Title3>Creating Content</Title3>
                                        </TreeItemLayout>
                                        <Tree>
                                            <TreeItem itemType='leaf'>
                                                <TreeItemLayout>
                                                    <ul>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Check the heading for the left side drawer and click on it</li>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Click "New Post" to create a bulletin</li>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Add your content with formatting options</li>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Submit for immediate posting</li>
                                                    </ul>
                                                </TreeItemLayout>
                                            </TreeItem>
                                        </Tree>
                                    </TreeItem>

                                    <TreeItem itemType='branch' value='tree-item-10'>
                                        <TreeItemLayout
                                            expandIcon={openItems.includes("tree-item-10") ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                        >
                                            <Title3>Interacting with Content</Title3>
                                        </TreeItemLayout>
                                        <Tree>
                                            <TreeItem itemType='leaf'>
                                                <TreeItemLayout>
                                                    <ul>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Add stars to a story</li>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> The total number of stars will be shown for both your story as well as others</li>
                                                    </ul>
                                                </TreeItemLayout>
                                            </TreeItem>
                                        </Tree>
                                    </TreeItem>
                                </Tree>
                            </TreeItem>
                        </Card>
                    </Tree>
                </div>
            </div>

            <div className="row">
                <div className="col-6">
                    <Tree
                        aria-label='technical-excellence'
                        className='technical-excellence'
                        openItems={openItems}
                        onOpenChange={handleOpenChange}
                    >
                        <Card className={styles.treeCard}>
                            <TreeItem itemType='branch' value="tree-item-11">
                                <TreeItemLayout
                                    expandIcon={openItems.includes("tree-item-11") ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                >
                                    <Title2 className={styles.titles}>Technical Excellence</Title2>
                                </TreeItemLayout>

                                <Tree>
                                    <TreeItem itemType='branch' value='tree-item-12'>
                                        <TreeItemLayout
                                            expandIcon={openItems.includes("tree-item-12") ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                        >
                                            <Title3>Architecture & Infrastructure</Title3>
                                        </TreeItemLayout>
                                        <Tree>
                                            <TreeItem itemType='leaf'>
                                                <TreeItemLayout>
                                                    <ul>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Secure and scalable architecture</li>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> High-performance infrastructure</li>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Data backup and recovery systems</li>
                                                    </ul>
                                                </TreeItemLayout>
                                            </TreeItem>
                                        </Tree>
                                    </TreeItem>

                                    <TreeItem itemType='branch' value='tree-item-13'>
                                        <TreeItemLayout
                                            expandIcon={openItems.includes("tree-item-13") ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                        >
                                            <Title3>Design & Security</Title3>
                                        </TreeItemLayout>
                                        <Tree>
                                            <TreeItem itemType='leaf'>
                                                <TreeItemLayout>
                                                    <ul>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Responsive design for all devices</li>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Regular security updates</li>
                                                    </ul>
                                                </TreeItemLayout>
                                            </TreeItem>
                                        </Tree>
                                    </TreeItem>
                                </Tree>
                            </TreeItem>
                        </Card>
                    </Tree>
                </div>
                <div className="col-6">
                    <Tree
                        aria-label='upcoming-features'
                        className='upcoming-features'
                        openItems={openItems}
                        onOpenChange={handleOpenChange}
                    >
                        <Card className={styles.treeCard}>
                            <TreeItem itemType='branch' value="tree-item-14">
                                <TreeItemLayout
                                    expandIcon={openItems.includes("tree-item-14") ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                >
                                    <Title2 className={styles.titles}>Upcoming Features</Title2>
                                </TreeItemLayout>

                                <Tree>
                                    <TreeItem itemType='branch' value='tree-item-15'>
                                        <TreeItemLayout
                                            expandIcon={openItems.includes("tree-item-15") ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                        >
                                            <Title3>User Management</Title3>
                                        </TreeItemLayout>
                                        <Tree>
                                            <TreeItem itemType='leaf'>
                                                <TreeItemLayout>
                                                    <ul>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Customizable user preferences</li>
                                                    </ul>
                                                </TreeItemLayout>
                                            </TreeItem>
                                        </Tree>
                                    </TreeItem>

                                    <TreeItem itemType='branch' value='tree-item-16'>
                                        <TreeItemLayout
                                            expandIcon={openItems.includes("tree-item-16") ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                        >
                                            <Title3>Content Management</Title3>
                                        </TreeItemLayout>
                                        <Tree>
                                            <TreeItem itemType='leaf'>
                                                <TreeItemLayout>
                                                    <ul>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> File attachments and media sharing</li>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Categorized content organization</li>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Search and filter capabilities</li>
                                                    </ul>
                                                </TreeItemLayout>
                                            </TreeItem>
                                        </Tree>
                                    </TreeItem>

                                    <TreeItem itemType='branch' value='tree-item-17'>
                                        <TreeItemLayout
                                            expandIcon={openItems.includes("tree-item-17") ? <SubtractSquareRegular /> : <AddSquareRegular />}
                                        >
                                            <Title3>Communication Tools</Title3>
                                        </TreeItemLayout>
                                        <Tree>
                                            <TreeItem itemType='leaf'>
                                                <TreeItemLayout>
                                                    <ul>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Real-time notifications</li>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Comment and discussion threads</li>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Email notifications</li>
                                                        <li className={styles.listItem}><ArrowCircleRightRegular /> Interactive feedback mechanisms</li>
                                                    </ul>
                                                </TreeItemLayout>
                                            </TreeItem>
                                        </Tree>
                                    </TreeItem>
                                </Tree>
                            </TreeItem>

                        </Card>

                    </Tree>
                </div>
            </div>
        </div >
    );
}

export default DescriptionComponent;